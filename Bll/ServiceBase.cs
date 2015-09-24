using System.Collections.Generic;

using YHCSheng.Dal;
using YHCSheng.Models;

namespace YHCSheng.Bll
{
    public class ServiceBase<T> where T: class {
        public IDao<T> Dao;
        
        public ServiceBase() {
            Dao = new EFClient<T>();
        }

        public IList<T> Retrieve(Dictionary<string, object> conditions = null) {
            return Dao.Find(conditions);
        }

        public T Create(T entity) {
            return Dao.Insert(entity);
        }

        public T Update(T entity) {
            return Dao.Update(entity);
        }

        public T GetById(int id) {
            return Dao.GetById(id);
        }

        public T GetBy<U>(string key, U value) {
            return Dao.GetBy(key, value);
        }

        public void Remove(T entity) {
            Dao.Delete(entity);
        }

        public void Remove(int id) {
            var entity = Dao.GetById(id);
            if(entity != null)
                Dao.Delete(Dao.GetById(id));
        }
    }
}