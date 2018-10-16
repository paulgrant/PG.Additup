using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Services;
using Moq;
using System;

namespace WebApi.Tests
{
    [TestClass]
    public class ExerciseTests
    {
        public ExerciseService _service;

        [TestInitialize()]
        public void Initialise()
        {
            IRepository<Exercise> repositoryMock = new Mock<IRepository<Exercise>>().Object;
            _service = new ExerciseService(repositoryMock);
        }


        [TestMethod]
        public void retrieve_standard_question()
        {
            var newExercise = _service.createExercise(string.Empty);
            Assert.IsTrue(newExercise.leftNumber <= 10);
            Assert.IsTrue(newExercise.mathOperator != Enums.Operator.divide);
            Assert.IsTrue(newExercise.mathOperator != Enums.Operator.multiply);
            Assert.IsFalse(string.IsNullOrEmpty(newExercise.userId.ToString()));
        }

        [TestMethod]
        public void retrieve_easy_question()
        {
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.easy);
            Assert.IsTrue(newExercise.leftNumber <= 10);
        }

        [TestMethod]
        public void retrieve_challenging_question()
        {
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.challenging);
            Assert.IsTrue(newExercise.leftNumber <= 50);
        }

        [TestMethod]
        public void retrieve_difficult_question()
        {
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.difficult);
            Assert.IsTrue(newExercise.leftNumber <= 100);
        }

        [TestMethod]
        public void retrieve_nightmare_question()
        {
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.nightmare);
            Assert.IsTrue(newExercise.leftNumber <= 1000);
        }

        [TestMethod]
        public void post_correct_easy_question()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = "4";
            newExercise.userId = Guid.NewGuid();
            _service.checkAnswer(newExercise);
            Assert.IsTrue(newExercise.correctAnswerGiven, "Answer is invalid");
        }

        [TestMethod]
        public void post_correct_easy_question_with_no_userId()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = "4";
            _service.checkAnswer(newExercise);
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void post_incorrect_easy_question()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = "5";
            newExercise.userId = Guid.NewGuid();
            _service.checkAnswer(newExercise);
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void retrieve_unique_question()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = "5";
            newExercise.userId = Guid.NewGuid();
            _service.checkAnswer(newExercise);
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void retrieve_unique_questionfor_two_users()
        {
            
        }

        [TestCleanup()]
        public void Cleanup()
        {

        }
    }
}
