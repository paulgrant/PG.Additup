using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Enums;
using Microsoft.Extensions.Logging;
using WebApi.Services.Interfaces;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private readonly IExerciseService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseController(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IExerciseService exerciseService)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("Exercices");
            _service = exerciseService;
            _unitOfWork = unitOfWork;
        }

        // GET api/exercise
        [HttpGet]
        public Exercise GetExercise(string userId, Difficulty difficulty = Difficulty.simple)
        {
            try
            {
                // TODO - check valid user 
                //if(!this.IsValidUser(userId)) { return StatusCode(HttpStatusCode.Unauthorized); }
                var _currentExercise = _service.createExercise(userId, difficulty);
                _unitOfWork.SaveChanges();
                return _currentExercise;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to generate exercise");
                return null;
            }
        }


        // POST api/exercise
        [HttpPost]
        public Exercise PostAnswer([FromBody]Exercise exercise)
        {
            try
            {
                var checkedEx = exercise;
                var savedExercise = _service.checkAnswer(checkedEx);
                _unitOfWork.SaveChanges();
                return savedExercise;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to generate exercise");
                throw new InvalidProgramException();
            }
        }
    }
}
