using Blog.Core.Entities;
using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repositories.Concretes
{
    public class Repository<T> :IRepository<T> where T : class, IEntitiyBase, new()
    {
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        private DbSet<T> Table { get => dbContext.Set<T>(); }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if(predicate != null)
                query = query.Where(predicate);
            if(includeProperties.Any())
                foreach(var item in includeProperties)
                    query = query.Include(item);
            return await query.ToListAsync();
        }

        public async Task AddAsync(T entitiy) // Task = void (asenkron halindeki void)
        {
           await Table.AddAsync(entitiy);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);

            return await query.SingleAsync();
            
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entitiy)
        {
            await Task.Run(() => Table.Update(entitiy)); 
            return entitiy;
        }

        public async Task DeleteAsync(T entitiy)
        {
            await Task.Run(() => Table.Remove(entitiy));

        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AllAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Table.CountAsync(predicate);
        }
    }
}
