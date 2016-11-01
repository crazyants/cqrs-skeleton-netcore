using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uranus.Commercial.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        void Commit();

        T GetRepository<T>() where T : class;

        void Dispose();
    }
}
