using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace web_api_challenge.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetByFilter(Expression<Func<T, bool>> condition);
        void Add(T entity);
        void Update(T entity);
        void UpdateByFilter(Expression<Func<T, bool>> condition, Action<T> update);
        void Delete(T entity);
        void DeleteByFilter(Expression<Func<T, bool>> condition);
        void Save();
    }
}
