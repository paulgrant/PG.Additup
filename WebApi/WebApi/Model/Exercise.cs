using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Enums;

namespace WebApi.Model
{
    public class Exercise
    {
        public int leftNumber;
        public int rightNumber;
        public Operator mathOperator;
        public bool correctAnswerGiven = false;

        public Exercise() { }

        // Method to create an exercise of type a + b = ?
        public static Exercise createExercise(Difficulty difficulty = Difficulty.simple)
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

        public static void checkAnswer(string value, ref Exercise currentExercise)
        {
            if(value == null)
            {
                currentExercise.correctAnswerGiven = false;
            }
            else
            {
                double.TryParse(value, out double answerValue);
                double _calculatedAnswer = 0;
                switch (currentExercise.mathOperator)
                {
                    case Operator.add:
                        _calculatedAnswer = (currentExercise.leftNumber + currentExercise.rightNumber);
                        break;
                    case Operator.subtract:
                        _calculatedAnswer = (currentExercise.leftNumber - currentExercise.rightNumber);
                        break;
                    case Operator.multiply:
                        _calculatedAnswer = (currentExercise.leftNumber * currentExercise.rightNumber);
                        break;
                    case Operator.divide:
                        _calculatedAnswer = (currentExercise.leftNumber / currentExercise.rightNumber);
                        break;
                }
                currentExercise.correctAnswerGiven = answerValue.Equals(_calculatedAnswer);

            }
            
        }
    }
}
