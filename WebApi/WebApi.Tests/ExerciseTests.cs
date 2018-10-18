using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Services;
using Moq;
using System;
using System.Collections.Generic;

namespace WebApi.Tests
{
    [TestClass]
    public class Exercisetests
    {
        public ExerciseService _service;

        [TestInitialize()]
        public void Initialise()
        {
        }


        [TestMethod]
        public void retrieve_standard_question()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);
            var newExercise = _service.createExercise(string.Empty);
            Assert.IsTrue(newExercise.leftNumber <= 10);
            Assert.IsTrue(newExercise.mathOperator != Enums.Operator.divide);
            Assert.IsTrue(newExercise.mathOperator != Enums.Operator.multiply);
            Assert.IsFalse(string.IsNullOrEmpty(newExercise.userId.ToString()));
        }

        [TestMethod]
        public void retrieve_easy_question()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.easy);
            Assert.IsTrue(newExercise.leftNumber <= 10);
        }

        [TestMethod]
        public void retrieve_challenging_question()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.challenging);
            Assert.IsTrue(newExercise.leftNumber <= 50);
        }

        [TestMethod]
        public void retrieve_difficult_question()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);
            var newExercise = _service.createExercise(string.Empty, Enums.Difficulty.difficult);
            Assert.IsTrue(newExercise.leftNumber <= 100);
        }

        [TestMethod]
        public void retrieve_nightmare_question()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);
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
            newExercise.answer = 4;
            newExercise.userId = Guid.NewGuid();

            var mock = new Mock<IExerciseRepository>();
            mock.Setup(m => m.Find(It.IsAny<int>())).Returns(newExercise);
            _service = new ExerciseService(mock.Object);
            _service.checkAnswer(newExercise);
            Assert.IsTrue(newExercise.correctAnswerGiven, "Answer is invalid");
        }

        [TestMethod]
        public void post_correct_easy_question_with_no_userId()
        {
            var newExercise = new Exercise();
            newExercise.exerciseId = 1;
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = 4;
            var mock = new Mock<IExerciseRepository>();
            mock.Setup(m => m.Find(It.IsAny<int>())).Returns(newExercise);
            _service = new ExerciseService(mock.Object);
            Assert.ThrowsException<ArgumentNullException>(() => _service.checkAnswer(newExercise));
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void post_incorrect_easy_question()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = 5;
            newExercise.userId = Guid.NewGuid();
            var mock = new Mock<IExerciseRepository>();
            mock.Setup(m => m.Find(It.IsAny<int>())).Returns(newExercise);
            _service = new ExerciseService(mock.Object);
            _service.checkAnswer(newExercise);
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void post_correct_answer_with_no_userId()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            newExercise.answer = 5;
            newExercise.userId = Guid.NewGuid();
            var mock = new Mock<IExerciseRepository>();
            mock.Setup(m => m.Find(It.IsAny<int>())).Returns(newExercise);
            _service = new ExerciseService(mock.Object);
            _service.checkAnswer(newExercise);
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void retrieve_unique_question_for_two_users()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);
            var newExercise = _service.createExercise(string.Empty);
            var newExercise1 = _service.createExercise(string.Empty);

            Assert.IsFalse(newExercise.userId.Equals(newExercise1.userId));
            Assert.IsFalse(newExercise.leftNumber == newExercise1.leftNumber 
                && newExercise.rightNumber == newExercise1.rightNumber 
                && newExercise.mathOperator == newExercise1.mathOperator);
            Assert.IsFalse(string.IsNullOrEmpty(newExercise.userId.ToString()));
            Assert.IsFalse(string.IsNullOrEmpty(newExercise1.userId.ToString()));
        }

        [TestMethod]
        public void retrieve_unique_question_for_two_users_with_duplicate_found()
        {
            var mock = new Mock<IExerciseRepository>();
            _service = new ExerciseService(mock.Object);

            //create first
            var newExercise = _service.createExercise(string.Empty);

            //mock detection of duplicate
            mock.SetupSequence(m => m.GetUnansweredMatch(It.IsAny<Exercise>()))
                .Returns(new List<Exercise>(){ newExercise })
                .Returns(new List<Exercise>() { });

            var newExercise1 = _service.createExercise(string.Empty);

            Assert.IsFalse(newExercise.userId.Equals(newExercise1.userId));
            Assert.IsFalse(newExercise.leftNumber == newExercise1.leftNumber
                && newExercise.rightNumber == newExercise1.rightNumber
                && newExercise.mathOperator == newExercise1.mathOperator);
            Assert.IsFalse(string.IsNullOrEmpty(newExercise.userId.ToString()));
            Assert.IsFalse(string.IsNullOrEmpty(newExercise1.userId.ToString()));
        }


        [TestCleanup()]
        public void Cleanup()
        {

        }
    }
}
