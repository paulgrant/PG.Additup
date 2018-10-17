using System;
using WebApi.Enums;
using WebApi.Helper;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ScoreService : Service<Score>, IScoreService
    {
        private readonly IScoreRepository _repository;

        public ScoreService(IScoreRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Score Create(Guid userId)
        {
            var newScore = new Score()
            {
                userId = userId,
                highScore = 0,
                level = GetScoreFromLevel(0)
            };
            Insert(newScore);
            return newScore;
        }

        private int GetScoreFromLevel(int score)
        {
            decimal _levelSize = 0;
            decimal.TryParse(ConfigurationManager.AppSetting["AppSettings:LevelSize"], out _levelSize);
            var calculatedLevel = (int)Math.Floor(score / _levelSize) + 1;
            return calculatedLevel;
        }

        public Score IncrementScore(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentOutOfRangeException("userId is empty");
            }
            var score = GetByUserId(userId);
            if (score == null)
            {
                score = Create(new Guid(userId));
            }
            else
            {
                score.highScore = score.highScore + 1;
                score.level = GetScoreFromLevel(score.highScore);
                Update(score);
            }
            return score;
        }

        public Score ResetScore(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentOutOfRangeException("userId is empty");
            }
            var score = GetByUserId(userId);
            score.level = (int)Level.Beginner;
            score.highScore = 0;
            Update(score);
            return score;
        }

        public Score GetByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentOutOfRangeException("userId is empty");
            }
            var userGuid = new Guid(userId);
            return _repository.GetByUserId(userGuid);
        }
    }
}
