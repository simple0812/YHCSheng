using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Entity;
using YHCSheng.Extensions;

namespace YHCSheng.Dal {
    public class EFClient<T> : IDao<T> where T : class {
        private readonly DbContext _context;
        private readonly Type[] _list = { typeof(ApplicationDbContext), typeof(LogDbContext) };

        public EFClient() {
            foreach (var each in _list) {
                var x = each.GetProperties().First(p => typeof (DbSet<T>) == p.PropertyType);

                if (null == x) continue;

                _context = Activator.CreateInstance(each) as DbContext;
                break;
            }

            if (null == _context) {
                throw new Exception("create dbcontext failed");
            }
        }

        public T Save(T entity) {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public bool Delete(T entity) {
            if (_context.Entry(entity) == null) return false;
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }

        public bool Delete(IList<T> entities) {
            _context.RemoveRange(entities);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteBy(string key, object value) {
            var entity = GetBy(key, value);
            if (entity == null) return false;

            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteById(int id) {
            return DeleteBy("Id", id);
        }

        public T Update(T entity) {
            var p = _context.Set<T>();
            p.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public T GetById(int id) {
            return GetBy("Id", id);
        }

        public T GetBy(string key, object value) {
            var parameter = Expression.Parameter(typeof (T), "p");
            var body = Expression.Equal(Expression.Property(parameter, key), Expression.Constant(value));
            var expression = Expression.Lambda<Func<T, bool>>(body, parameter);
            return _context.Set<T>().Where(expression).FirstOrDefault();
        }

        public List<T> GetAll() {
            return _context.Set<T>().ToList();
        }

        //true : asc   false : desc
        public List<T> GetByCondition(Expression< Func<T, bool>> conditions, Dictionary<string, bool> order =null) {
            var parameter = Expression.Parameter(typeof (T), "x");
            if(order == null || order.Count == 0) return _context.Set<T>().Where(conditions).ToList<T>();
            if (null == conditions) conditions = arg => true;
            var keys = order.Keys.ToList();
            var list = _context.Set<T>().Where(conditions);

            for (int i = 0, len = keys.Count; i < len; i ++) {
                var lamda =
                    Expression.Lambda<Func<T, object>>(
                        Expression.Convert(Expression.Property(parameter, keys[i]), typeof (object)), parameter);
                
                if (i == 0) {
                    list = order[keys[i]] ? list?.OrderBy(lamda) : list?.OrderByDescending(lamda);
                } else {
                    list = order[keys[i]] ? ((IOrderedQueryable<T>)list).ThenBy(lamda) : ((IOrderedQueryable<T>)list).ThenByDescending(lamda);
                }
            }

            return list.ToList();
        }

        //true : asc   false : desc
        public List<T> GetPageList(int pageSize, int pageIndex, out int recordCount, Expression<Func<T, bool>> where, Dictionary<string, bool> order) {
            var parameter = Expression.Parameter(typeof(T), "x");
            var firstNum = (pageIndex - 1) * pageSize;

            var keys = order.Keys.ToList();
            
            var list = null == where ? _context.Set<T>() : _context.Set<T>().Where(where);
            recordCount = list.Count();

            for (int i = 0, len = keys.Count; i < len; i++) {
                var lamda =
                    Expression.Lambda<Func<T, object>>(
                        Expression.Convert(Expression.Property(parameter, keys[i]), typeof(object)), parameter);

                if (i == 0) {
                    list = order[keys[i]] ? list.OrderBy(lamda) : list.OrderByDescending(lamda);
                } else {
                    list = order[keys[i]] ? ((IOrderedQueryable<T>)list).ThenBy(lamda) : ((IOrderedQueryable<T>)list).ThenByDescending(lamda);
                }
            }

            return list.Skip(firstNum).Take(pageSize).ToList();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}