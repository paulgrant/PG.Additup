using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
    }
}
