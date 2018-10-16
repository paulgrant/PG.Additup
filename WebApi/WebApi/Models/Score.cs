using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Enums;

namespace WebApi.Model
{
    public class Exercise
    {
        [Key]
        public int scoreUserId { get; set; }
        public Guid userId { get; set; }
        public string level { get; set; }
        public int highScore { get; set; }
    }
}
