using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;

        //private readonly Dictionary<string, object> _repositories ;
        private readonly ConcurrentDictionary<string, object> _repositories ;
        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
            //_repositories = new Dictionary<string, object>();
            _repositories = new ConcurrentDictionary<string, object>();
        }
        //public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        //{
        //    var type = typeof(TEntity).Name;
        //    var flag = _repositories.ContainsKey(type);
        //    if (!flag)
        //    {
        //        var repository = new GenericRepository<TEntity, Tkey>(_context);
        //        _repositories.Add(type, repository);
        //    }
        //    return (IGenericRepository<TEntity, Tkey>) _repositories[type];
        //}

        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            return (IGenericRepository<TEntity, Tkey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, Tkey>(_context));
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
