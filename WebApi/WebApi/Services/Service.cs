using System.Threading.Tasks;
using WebApi.Services.Interfaces;
using WebApi.Repository;

namespace WebApi.Services
{
    public abstract class Service<TEntity> : IService<TEntity>
    {
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public TEntity Get(int id)
        {
            return _repository.Find(id);
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}