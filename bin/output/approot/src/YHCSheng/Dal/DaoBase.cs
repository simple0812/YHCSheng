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
            var set = context.Set<T>();
            set.Attach(entity);
            context.Entry<T>(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity;
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

        /*
        public List<T> FindAll(Expression<Func<T, bool>> conditions = null) {
            Console.WriteLine("...");
            if (conditions == null)
                return context.Set<T>().ToList();
            else
                return context.Set<T>().Where(conditions).ToList();
        }
        */


        public void Dispose() {
            this.context.Dispose();
        }
    }
}
