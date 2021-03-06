﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Repository
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        IList<Exercise> GetUnansweredMatch(Exercise exercise);
        IList<Exercise> FindByUserId(Guid userId);
    }
}
