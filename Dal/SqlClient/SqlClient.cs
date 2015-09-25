using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace YHCSheng.Dal
{
    public class Sql<T> : IDao<T> where T : class {
        private readonly DbContext _context = new LogDbContext();
        public T Save(T entity) {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(T entity) {
            if(_context.Entry<T>(entity) == null) return false;
            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteBy(string key, object value) {
            var entity = GetBy(key, value);
            if(entity == null) return false;

            _context.Entry<T>(entity).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteById(int id) {
            string tbName = typeof(T).FullName.Replace(typeof(T).Namespace + ".", ""  );

            string sql = "DELETE FROM {0} WHERE Id = {1}";

            return DeleteBy("Id", id);
        }

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
        
        public List<T> GetAll() {
            return _context.Set<T>().ToList();
        }

        public List<T> GetByCondition(Func<T, bool> conditions) {
            return _context.Set<T>().Where(conditions).ToList();
        }

        public List<T> GetByCondition(Dictionary<string, object> conditions) {
            if(conditions == null || conditions.Count == 0) {
                return _context.Set<T>().ToList();
            }

            Expression p = null;
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach(var each in conditions) {
                var body = Expression.Equal(Expression.Property(parameter, each.Key), Expression.Constant(each.Value));
                p = p == null ? body : p.AndAlso(body);
            }

            return _context.Set<T>().Where(Expression.Lambda<Func<T, bool>>(p, parameter)).ToList();
        }

        public List<T> GetByPaged(int firstnum, int pagesize, out int recordcount, Dictionary<string, object> conditions) {
            recordcount = 0;
            return _context.Set<T>().ToList();
        }

        public List<T> GetByPaged(int firstnum, int pagesize, out int recordcount, Func<T, bool> conditions) {
            recordcount = 0;
            return _context.Set<T>().ToList();
        }


        public void Dispose() {
            this._context.Dispose();
        }
    }
}
