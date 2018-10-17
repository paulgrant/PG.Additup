using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;

namespace WebApi.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        IRepository<Exercise> _repository;
        public ExerciseRepository(IRepository<Exercise> repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(Exercise entity)
        {
            _repository.Delete(entity);
        }

        public Exercise Find(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<Exercise> GetAll()
        {
            return _repository.GetAll();
        }

        public IList<Exercise> GetUnansweredMatch(Exercise exercise)
        {
            return _repository
                .Queryable()
                .Where(u => u.leftNumber == exercise.leftNumber
                    && u.mathOperator == exercise.mathOperator
                    && u.rightNumber == exercise.rightNumber
                    && string.IsNullOrEmpty(u.answer)
                    && u.correctAnswerGiven == false)
                .ToList();
        }

        public void Insert(Exercise entity)
        {
            _repository.Insert(entity);
        }

        public IQueryable<Exercise> Queryable()
        {
            return _repository.Queryable();
        }

        public void Update(Exercise entity)
        {
            _repository.Update(entity);
        }
    }
}
