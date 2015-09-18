using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using System.Diagnostics;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;

namespace YHCSheng.Dal
{
    public class DaoBase<T> : IDao<T> where T : class {
        public ApplicationDbContext context = new ApplicationDbContext();

        public T Update(T entity) {
            var p = context.Set<T>();
            p.Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }

        public T GetById(int id) {
            return GetBy("Id", id);
        }

        public T GetBy(string key, object value) {
            var parameter = Expression.Parameter(typeof(T), "p");
            var body = Expression.Equal(Expression.Property(parameter, key), Expression.Constant(value));
            var expression = Expression.Lambda<Func<T, bool>>(body, parameter);
            return context.Set<T>().Where(expression).FirstOrDefault();
        }

        public T Insert(T entity) {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void Delete(T entity) {
            context.Entry<T>(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public List<T> FindAll() {
            return context.Set<T>().ToList();
        }

        public List<T> Find(Dictionary<string, object> conditions) {
            if(conditions == null || conditions.Count == 0) {
                return context.Set<T>().ToList();
            }

            Expression p = null;
            var parameter = Expression.Parameter(typeof(T), "x");

            foreach(var each in conditions) {
                var body = Expression.Equal(Expression.Property(parameter, each.Key), Expression.Constant(each.Value));
                if(p == null) {
                    p = body;
                } else {
                    p = p.AndAlso(body);
                }
            }

            return context.Set<T>().Where(Expression.Lambda<Func<T, bool>>(p, parameter)).ToList();
        }

        public void Dispose() {
            this.context.Dispose();
        }
    }
}
