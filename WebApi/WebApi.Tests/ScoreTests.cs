using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Services;
using Moq;
using System;

namespace WebApi.Tests
{
    [TestClass]
    public class ScoreTests
    {
        public ScoreService _service;

        [TestInitialize()]
        public void Initialise()
        {
            
        }


        [TestMethod]
        public void get_new_score()
        {
            var mock = new Mock<IScoreRepository>();
            _service = new ScoreService(mock.Object);
            var userId = Guid.NewGuid();
            var score = _service.Create(userId);
            Assert.IsTrue(score.userId == userId);
            Assert.IsTrue(score.level == 1);
            Assert.IsTrue(score.highScore == 0);
            mock.Verify(sc => sc.Insert(score));
        }

        [TestMethod]
        public void reset_score()
        {
            var score = new Score()
            {
                userId = Guid.NewGuid(),
                level = 2,
                highScore = 6
            };
            var mock = new Mock<IScoreRepository>();
            mock.Setup(m => m.GetByUserId(It.Is<Guid>(i=>i == score.userId))).Returns(score);
            _service = new ScoreService(mock.Object);
            
            var _resetScore = _service.ResetScore(score.userId.ToString());
            Assert.IsTrue(_resetScore.level.Equals(0));
            Assert.IsTrue(_resetScore.highScore.Equals(0));
            mock.Verify(sc => sc.Update(It.IsAny<Score>()));
        }

        [TestMethod]
        public void reset_score_for_invalid_user()
        {
            var score = new Score()
            {
                userId = Guid.Empty,
                level = 2,
                highScore = 6
            };
            var mock = new Mock<IScoreRepository>();

            mock.Setup(m => m.GetByUserId(It.IsAny<Guid>())).Returns(score);
            _service = new ScoreService(mock.Object);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _service.ResetScore(""));
        }

        [TestMethod]
        public void increment_score_for_user()
        {
            var score = new Score()
            {
                userId = Guid.NewGuid(),
                level = 2,
                highScore = 4
            };
            var mock = new Mock<IScoreRepository>();
            mock.Setup(m => m.GetByUserId(It.Is<Guid>(i => i == score.userId))).Returns(score);
            _service = new ScoreService(mock.Object);

            var _resetScore = _service.IncrementScore(score.userId.ToString());
            Assert.IsTrue(_resetScore.level.Equals(2));
            Assert.IsTrue(_resetScore.highScore.Equals(5));
            mock.Verify(sc => sc.Update(It.IsAny<Score>()));
        }

        [TestMethod]
        public void increment_score_next_level_for_user()
        {
            var score = new Score()
            {
                userId = Guid.NewGuid(),
                level = 3,
                highScore = 8
            };
            var mock = new Mock<IScoreRepository>();
            mock.Setup(m => m.GetByUserId(It.Is<Guid>(i => i == score.userId))).Returns(score);
            _service = new ScoreService(mock.Object);

            var _resetScore = _service.IncrementScore(score.userId.ToString());
            Assert.IsTrue(_resetScore.level.Equals(4));
            Assert.IsTrue(_resetScore.highScore.Equals(9));
            mock.Verify(sc => sc.Update(It.IsAny<Score>()));
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _service = null;
        }
    }
}
