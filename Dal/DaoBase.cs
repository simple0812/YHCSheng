using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace YHCSheng.Dal
{
    public class DaoBase<T> : IDao<T> where T : class {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public T Update(T entity) {
            var p = _context.Set<T>();
            p.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public T GetById(int id) {
            return GetBy("Id", id);
        }

        public T GetBy(string key, object value) {
            var parameter = Expression.Parameter(typeof(T), "p");
            var body = Expression.Equal(Expression.Property(parameter, key), Expression.Constant(value));
            var expression = Expression.Lambda<Func<T, bool>>(body, parameter);
            return _context.Set<T>().Where(expression).FirstOrDefault();
        }

        public T Insert(T entity) {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity) {
            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public List<T> FindAll() {
            return _context.Set<T>().ToList();
        }

        public List<T> Find(Dictionary<string, object> conditions) {
            if(conditions == null || conditions.Count == 0) {
                return _context.Set<T>().ToList();
            }

            Expression p = null;
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach(var each in conditions)
            {
                var body = Expression.Equal(Expression.Property(parameter, each.Key), Expression.Constant(each.Value));
                p = p == null ? body : p.AndAlso(body);
            }

            return _context.Set<T>().Where(Expression.Lambda<Func<T, bool>>(p, parameter)).ToList();
        }

        public void Dispose() {
            this._context.Dispose();
        }
    }
}
