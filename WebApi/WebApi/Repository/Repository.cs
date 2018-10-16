using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private readonly ExerciseContext _dataContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IDataContext dataContext)
        {
            _dataContext = dataContext as ExerciseContext;
            _dbSet = _dataContext.Set<TEntity>();

        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
        }


        public void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {

            if (_dataContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dataContext.Entry(entity).State = EntityState.Deleted;

            _dbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            //_dbSet.Update(entity);
        }


        public async Task UpdateAsync(TEntity entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return;
        }
        public TEntity Find(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }
    }
}
