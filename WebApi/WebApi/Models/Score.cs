using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Score
    {
        [Key]
        public Guid userId { get; set; }
        public int level { get; set; }
        public int highScore { get; set; }
    }
}
