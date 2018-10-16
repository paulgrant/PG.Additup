using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Enums;

namespace WebApi.Model
{
    public class Exercise
    {
        [Key]
        public int exerciseId { get; set; }
        public int leftNumber { get; set; }
        public int rightNumber { get; set; }
        public Operator mathOperator { get; set; }
        public string answer { get; set; }
        public bool correctAnswerGiven { get;set; }
        public Guid userId { get; set; }
    }
}
