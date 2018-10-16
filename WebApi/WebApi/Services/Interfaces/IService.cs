using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services.Interfaces
{
    public interface IService<TEntity>
    {
        void Insert(TEntity entity);

        void Delete(TEntity entity);

        void Delete(int id);

        TEntity Get(int id);

        void Update(TEntity entity);
    }
}
