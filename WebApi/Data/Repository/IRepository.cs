using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        void Insert(TEntity entity);

        void Delete(int id);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        TEntity Find(int id);

        IQueryable<TEntity> Queryable();
    }
}
