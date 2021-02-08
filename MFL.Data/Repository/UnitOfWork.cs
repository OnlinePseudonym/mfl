using MFL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MFL.Data.Repository
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private MFLContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(MFLContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);

            if (_repositories.Keys.Contains(type))
            {
                return _repositories[type] as IRepository<T>;
            }

            var repository = new Repository<T>(_context);

            _repositories.Add(type, repository);

            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
