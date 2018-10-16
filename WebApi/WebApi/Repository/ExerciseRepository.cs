using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Repository
{
    public static class ExerciseRepository
    {
        public static IList<Exercise> GetUnansweredMatch(this IRepository<Exercise> repository, Exercise exercise)
        {
            return repository
                .Queryable()
                .Where(u => u.leftNumber == exercise.leftNumber
                    && u.mathOperator == exercise.mathOperator
                    && u.rightNumber == exercise.rightNumber)
                .ToList();
        }
    }
}
