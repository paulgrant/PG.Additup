using System;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Enums;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private Exercise _currentExercise;
        
        public ExerciseController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("Exercices");
        }

        // GET api/exercise
        [HttpGet]
        public Exercise GetExercise(Difficulty difficulty = Difficulty.simple)
        {
            try
            {
                //if (!this.IsValidUser())
                //{
                //    return StatusCode(HttpStatusCode.Unauthorized);
                //}
                _currentExercise = Exercise.createExercise(difficulty);
                return _currentExercise;
            }
            catch (Exception exc)
            {
                //log exception here?
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
                //if (!this.IsValidUser())
                //{
                //    return StatusCode(HttpStatusCode.Unauthorized);
                //}
                var checkedEx = exercise;
                Exercise.checkAnswer(checkedEx);
                return checkedEx;
            }
            catch (Exception exc)
            {
                //log exception here?
                _logger.LogError(exc, "Failed to generate exercise");
                throw new InvalidProgramException();
            }
        }
    }
}
