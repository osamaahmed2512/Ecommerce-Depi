﻿using eCommerce_dpei.Data;
using eCommerce_dpei.repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce_dpei.Models
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly EcommerceContext _Context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EcommerceContext context)
        {
            _Context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdasync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
           
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
           
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}
