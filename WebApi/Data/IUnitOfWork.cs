using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}
