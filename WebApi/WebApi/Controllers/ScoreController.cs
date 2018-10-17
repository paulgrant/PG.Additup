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
    public class ScoreController : Controller
    {
        ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private readonly IScoreService _service;
        private readonly IUnitOfWork _unitOfWork;

        public ScoreController(ILoggerFactory loggerFactory, IUnitOfWork unitOfWork, IScoreService service)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("Scores");
            _service = service;
            _unitOfWork = unitOfWork;
        }

        // GET api/exercise
        [HttpGet]
        public Score Get(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentOutOfRangeException("userId is empty");
                };
                var _score = _service.GetByUserId(userId);
                return _score;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "Failed to generate exercise");
                return null;
            }
        }
    }
}
