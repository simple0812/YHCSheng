using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;
using System;

namespace YHCSheng.Dal {
    public interface IDao<T> where T : class {
        T Save(T entity);

        bool Delete(T entity);
        bool DeleteById(int id);
        bool DeleteBy(string key, object value);

        T Update(T entity);

        T GetById(int id);
        T GetBy(string key, object value);

        List<T> GetAll();

        // 排序 true : asc   false : desc
        List<T> GetByCondition(Expression<Func<T, bool>> where, Dictionary<string, bool> order);
        List<T> GetPageList(int pageSize, int pageIndex, out int recordCount, Expression<Func<T, bool>> where, Dictionary<string, bool> order);
    }
}