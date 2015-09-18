using System.Collections.Generic;

namespace YHCSheng.Dal
{
    public interface IDao<T> where T : class {
        T Update(T entity);
        T Insert(T entity);
        T GetById(int id);
        T GetBy(string key, object value);
        void Delete(T entity);
        List<T> FindAll();
        List<T> Find(Dictionary<string, object> conditions);
    }
}