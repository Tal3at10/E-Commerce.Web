using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec, bool trackChange = false);
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> GetCountAsync(ISpecifications<TEntity, Tkey> spec);
    }
}
