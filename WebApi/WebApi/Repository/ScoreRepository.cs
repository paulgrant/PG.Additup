using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Model;

namespace WebApi.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        IRepository<Score> _repository;
        public ScoreRepository(IRepository<Score> repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(Score entity)
        {
            _repository.Delete(entity);
        }

        public Score Find(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<Score> GetAll()
        {
            return _repository.GetAll();
        }

        public Score GetByUserId(Guid userId)
        {
            return _repository
                .Queryable()
                .FirstOrDefault(u => u.userId == userId);
        }

        public void Insert(Score entity)
        {
            _repository.Insert(entity);
        }

        public IQueryable<Score> Queryable()
        {
            return _repository.Queryable();
        }

        public void Update(Score entity)
        {
            _repository.Update(entity);
        }
    }
}
