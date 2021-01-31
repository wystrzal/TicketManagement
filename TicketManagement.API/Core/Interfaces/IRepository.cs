using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<List<T>> GetByConditionToList(Expression<Func<T, bool>> condition);
        Task<List<T>> GetByConditionWithIncludeToList<Tprop>(Expression<Func<T, bool>> condition, Expression<Func<T, Tprop>> include);
        Task<T> GetByConditionFirst(Expression<Func<T, bool>> condition);
        Task<T> GetByConditionWithIncludeFirst<Tprop>(Expression<Func<T, bool>> condition, Expression<Func<T, Tprop>> include);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
    }
}
