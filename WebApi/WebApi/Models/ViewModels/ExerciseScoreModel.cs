using System;
using WebApi.Enums;

namespace WebApi.Models.ViewModels
{
    public class ExerciseScoreModel
    {
        public int exerciseId { get; set; }
        public int leftNumber { get; set; }
        public int rightNumber { get; set; }
        public Operator mathOperator { get; set; }
        public double? answer { get; set; }
        public bool correctAnswerGiven { get; set; }
        public Guid userId { get; set; }
        public int level { get; set; }
        public int highScore { get; set; }
    }
}
