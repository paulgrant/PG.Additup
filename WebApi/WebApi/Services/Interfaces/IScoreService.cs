using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Enums;
using WebApi.Model;

namespace WebApi.Services.Interfaces
{
    public interface IScoreService : IService<Score>
    {
        Score Create(Guid userId);

        Score IncrementScore(string userId);

        Score ResetScore(string userId);

        Score GetByUserId(string userId);
    }
}
