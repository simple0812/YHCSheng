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
        List<T> GetByCondition(Dictionary<string, object> conditions); //只能处理equal
        List<T> GetByCondition(Func<T, bool> conditions);

        List<T> GetByPaged(int firstnum, int pagesize, out int recordcount, Dictionary<string, object> conditions);
        List<T> GetByPaged(int firstnum, int pagesize, out int recordcount, Func<T, bool> conditions);
    }
}