using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Repository
{
    public interface IScoreRepository : IRepository<Score>
    {
        Score GetByUserId(Guid userId);
    }
}
