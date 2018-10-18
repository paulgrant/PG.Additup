using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Enums;
using Microsoft.Extensions.Logging;
using WebApi.Services.Interfaces;
using WebApi.Data;
using WebApi.Models.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/exercise")]
    public class ExerciseController : Controller
    {
        ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private readonly IExerciseService _service;
        private readonly IScoreService _scoreService;
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseController(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IExerciseService exerciseService, IScoreService scoreService)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("Exercices");
            _service = exerciseService;
            _scoreService = scoreService;
            _unitOfWork = unitOfWork;
        }

        // GET api/exercise
        [HttpGet("{id?}")]
        public IActionResult GetExercise(string id = null)
        {
            try
            {
                var _currentExercise = _service.createExercise(id);
                _unitOfWork.SaveChanges();
                var newScore = _scoreService.GetByUserId(_currentExercise.userId.ToString());
                var response = new ExerciseScoreModel()
                {
                    exerciseId = _currentExercise.exerciseId,
                    leftNumber = _currentExercise.leftNumber,
                    rightNumber = _currentExercise.rightNumber,
                    mathOperator = _currentExercise.mathOperator,
                    answer = _currentExercise.answer,
                    userId = _currentExercise.userId,
                    level = newScore !=null ? newScore.level : 1,
                    highScore = newScore != null ? newScore.highScore : 0
                };
                return Ok(response);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to generate exercise");
                return StatusCode(500);
            }
        }


        // POST api/exercise
        [HttpPost]
        public IActionResult PostAnswer([FromBody]Exercise exercise)
        {
            try
            {
                var checkedEx = exercise;
                var savedExercise = _service.checkAnswer(checkedEx);
                Score newScore = null;
                if(savedExercise.correctAnswerGiven)
                {
                    newScore = _scoreService.IncrementScore(savedExercise.userId.ToString());
                }
                
                _unitOfWork.SaveChanges();
                var response = new ExerciseScoreModel()
                {
                    exerciseId = savedExercise.exerciseId,
                    leftNumber = savedExercise.leftNumber,
                    rightNumber = savedExercise.rightNumber,
                    mathOperator = savedExercise.mathOperator,
                    answer = savedExercise.answer,
                    userId = savedExercise.userId,
                    correctAnswerGiven = savedExercise.correctAnswerGiven,
                    level = newScore.level,
                    highScore = newScore.highScore
                };
                return Ok(response);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to generate exercise");
                //throw new InvalidProgramException();
                return StatusCode(500);
            }
        }
    }
}
