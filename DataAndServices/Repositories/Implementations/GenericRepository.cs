using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using web_api_challenge.Repositories.Interfaces;

namespace web_api_challenge.Repositories.Implementations
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly JoffroyChallengeContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(JoffroyChallengeContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public IEnumerable<T> GetByFilter(Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition).ToList();
        }

      

        public void UpdateByFilter(Expression<Func<T, bool>> condition, Action<T> update)
        {
            _context.Set<T>().Where(condition).ToList().ForEach(update);
        }

        public void DeleteByFilter(Expression<Func<T, bool>> condition)
        {
            _context.Set<T>().RemoveRange(_context.Set<T>().Where(condition));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
