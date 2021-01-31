using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;

namespace TicketManagement.API.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext dataContext;

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Add(T entity)
        {
            dataContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            dataContext.Set<T>().Update(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await dataContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetByConditionToList(Expression<Func<T, bool>> condition)
        {
            return await dataContext.Set<T>().Where(condition).ToListAsync();
        }

        public async Task<T> GetByConditionFirst(Expression<Func<T, bool>> condition)
        {
            return await dataContext.Set<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dataContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByConditionWithIncludeFirst<Tprop>(Expression<Func<T, bool>> condition, Expression<Func<T, Tprop>> include)
        {
            return await dataContext.Set<T>().Include(include).Where(condition).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetByConditionWithIncludeToList<Tprop>(Expression<Func<T, bool>> condition, Expression<Func<T, Tprop>> include)
        {
            return await dataContext.Set<T>().Include(include).Where(condition).ToListAsync();
        }
    }
}
