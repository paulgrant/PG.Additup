using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Enums;
using WebApi.Model;
using WebApi.Repository;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class ExerciseService : Service<Exercise>, IExerciseService
    {
        private const int maxTriesForFindingNoMatch = 3;
        private IExerciseRepository _repository;
        public ExerciseService(IExerciseRepository repository) : base(repository)
        {
            _repository = repository;
        }

        // Method to create an exercise of type a + b = ?
        public Exercise createExercise(string userId, Difficulty difficulty = Difficulty.simple)
        {
            Exercise newExercise = GenerateUniqueExercise(difficulty);
            newExercise.userId = GenerateUniqueUserId(userId);
            Insert(newExercise);
            return newExercise;
        }

        private Guid GenerateUniqueUserId(string userId)
        {
            if (!string.IsNullOrEmpty(userId)) return new Guid(userId);

            var tries = 1;
            var uniqueId = false;
            Guid _userId;
            while (uniqueId == false && tries <= maxTriesForFindingNoMatch)
            {
                _userId = Guid.NewGuid();
                //check that the question is not a duplicate of a current unanswered question.
                var matches = _repository.FindByUserId(_userId);
                if (matches == null || matches.Count() == 0)
                {
                    uniqueId = true;
                }
                tries++;
            }
            return _userId;
        }

        private Exercise GenerateUniqueExercise(Difficulty difficulty)
        {
            var tries = 1;
            var uniqueQuestion = false;
            Exercise newExercise = null;
            while (uniqueQuestion == false && tries <= maxTriesForFindingNoMatch)
            {
                newExercise = GenerateNewQuestion(difficulty);
                //check that the question is not a duplicate of a current unanswered question.
                var matches = _repository.GetUnansweredMatch(newExercise);
                if (matches == null || matches.Count() == 0)
                {
                    uniqueQuestion = true;
                }
                tries++;
            }

            return newExercise;
        }

        private Exercise GenerateNewQuestion(Difficulty difficulty)
        {
            var newExercise = new Exercise();
            var rnd = new Random();
            switch (difficulty)
            {
                case Difficulty.simple:
                    newExercise.leftNumber = rnd.Next(1, 11);
                    newExercise.rightNumber = rnd.Next(1, 11);
                    newExercise.mathOperator = Operator.add;// (Operator)rnd.Next(0, 2);
                    break;
                case Difficulty.challenging:
                    newExercise.leftNumber = rnd.Next(1, 11);
                    newExercise.rightNumber = rnd.Next(1, 11);
                    newExercise.mathOperator = (Operator)rnd.Next(0, 4);
                    break;
                case Difficulty.difficult:
                    newExercise.leftNumber = rnd.Next(1, 101);
                    newExercise.rightNumber = rnd.Next(1, 101);
                    newExercise.mathOperator = (Operator)rnd.Next(0, 4);
                    break;
                case Difficulty.nightmare:
                    newExercise.leftNumber = rnd.Next(1, 1001);
                    newExercise.rightNumber = rnd.Next(1, 1001);
                    newExercise.mathOperator = (Operator)rnd.Next(0, 4);
                    break;
                default:
                case Difficulty.easy:
                    break;
            }

            return newExercise;
        }

        public Exercise checkAnswer(Exercise currentExercise)
        {
            if (currentExercise.userId == Guid.Empty)
            {
                throw new ArgumentNullException("User is not valid");
            }

            var _exercise = Get(currentExercise.exerciseId);
            if(_exercise == null)
            {
                throw new ArgumentNullException("Exercise is not valid");
            }

            if (_exercise.userId != currentExercise.userId)
            {
                throw new ArgumentNullException("User and/or Exercise is not valid");
            }

            if (currentExercise.answer == null)
            {
                throw new ArgumentNullException("Answer is not valid");
            }

            _exercise.answer = currentExercise.answer;
            double _calculatedAnswer = 0;
            switch (currentExercise.mathOperator)
            {
                case Operator.add:
                    _calculatedAnswer = (_exercise.leftNumber + _exercise.rightNumber);
                    break;
                case Operator.subtract:
                    _calculatedAnswer = (_exercise.leftNumber - _exercise.rightNumber);
                    break;
                case Operator.multiply:
                    _calculatedAnswer = (_exercise.leftNumber * _exercise.rightNumber);
                    break;
                case Operator.divide:
                    _calculatedAnswer = (_exercise.leftNumber / _exercise.rightNumber);
                    break;
            }
            _exercise.correctAnswerGiven = currentExercise.answer.Equals(_calculatedAnswer);
            Update(_exercise);
            return _exercise;
        }
    }
}
