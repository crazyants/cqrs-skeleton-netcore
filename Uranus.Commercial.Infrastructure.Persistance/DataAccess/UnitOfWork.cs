using Microsoft.EntityFrameworkCore;
using System;
using Uranus.Commercial.Infrastructure.Data;
using Uranus.Commercial.Infrastructure.Persistance.Context;

namespace Uranus.Commercial.Infrastructure.Persistance.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext dbContext) 
            : this(dbContext)
        {
            this._serviceProvider = serviceProvider;
        }

        #region [ IUnitOfWork Members ]

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public T GetRepository<T>() where T : class
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }

        #endregion
    }
}
