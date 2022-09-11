using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestFinal.DAL.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);
        List<TEntity> GetByCondition(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetById(int id);
        Task<TEntity> GetById(string id);
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task DeleteById(int id);
    }
}
