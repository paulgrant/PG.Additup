using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Enums;
using WebApi.Model;

namespace WebApi.Services.Interfaces
{
    public interface IExerciseService : IService<Exercise>
    {
        Exercise createExercise(string userId, Difficulty difficulty = Difficulty.simple);

        Exercise checkAnswer(Exercise currentExercise);
    }
}
