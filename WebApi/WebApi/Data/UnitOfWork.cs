using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDataContext _dataContext;
        private bool _disposed;

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // free other managed objects that implement
                    // IDisposable only

                    try
                    {

                        if (_dataContext != null)
                        {
                            _dataContext.Dispose();
                            _dataContext = null;
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // do nothing, the objectContext has already been disposed
                    }
                }

                _disposed = true;
            }
        }
    }
}
