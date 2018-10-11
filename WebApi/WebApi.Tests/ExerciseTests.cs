using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Model;

namespace WebApi.Tests
{
    [TestClass]
    public class ExerciseTests
    {
        [TestInitialize()]
        public void Initialise()
        {

        }


        [TestMethod]
        public void retrieve_standard_question()
        {
            var newExercise = Exercise.createExercise();
            Assert.IsTrue(newExercise.leftNumber <= 10);
            Assert.IsTrue(newExercise.mathOperator != Enums.Operator.divide);
            Assert.IsTrue(newExercise.mathOperator != Enums.Operator.multiply);
        }

        [TestMethod]
        public void retrieve_easy_question()
        {
            var newExercise = Exercise.createExercise(Enums.Difficulty.easy);
            Assert.IsTrue(newExercise.leftNumber <= 10);
        }

        [TestMethod]
        public void retrieve_challenging_question()
        {
            var newExercise = Exercise.createExercise(Enums.Difficulty.challenging);
            Assert.IsTrue(newExercise.leftNumber <= 50);
        }

        [TestMethod]
        public void retrieve_difficult_question()
        {
            var newExercise = Exercise.createExercise(Enums.Difficulty.difficult);
            Assert.IsTrue(newExercise.leftNumber <= 100);
        }

        [TestMethod]
        public void retrieve_nightmare_question()
        {
            var newExercise = Exercise.createExercise(Enums.Difficulty.nightmare);
            Assert.IsTrue(newExercise.leftNumber <= 1000);
        }

        [TestMethod]
        public void post_correct_easy_question()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            var answerValue = "4";
            Exercise.checkAnswer(answerValue, ref newExercise);
            Assert.IsTrue(newExercise.correctAnswerGiven);
        }

        [TestMethod]
        public void post_incorrect_easy_question()
        {
            var newExercise = new Exercise();
            newExercise.leftNumber = 2;
            newExercise.rightNumber = 2;
            newExercise.mathOperator = Enums.Operator.add;
            var answerValue = "5";
            Exercise.checkAnswer(answerValue, ref newExercise);
            Assert.IsFalse(newExercise.correctAnswerGiven);
        }

        [TestCleanup()]
        public void Cleanup()
        {

        }
    }
}
