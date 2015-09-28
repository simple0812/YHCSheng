using System.Collections.Generic;
using YHCSheng.Dal;

namespace YHCSheng.Bll {
    public class ServiceBase<T> where T : class {
        private readonly IDao<T> _dao;

        protected ServiceBase() {
            _dao = new EFClient<T>(); //SqlClient<T>();
        }

        public IList<T> Retrieve(Dictionary<string, object> conditions = null) {
            return _dao.GetByCondition(conditions);
        }

        public T Create(T entity) {
            return _dao.Save(entity);
        }

        public T Update(T entity) {
            return _dao.Update(entity);
        }

        public T GetById(int id) {
            return _dao.GetById(id);
        }

        public T GetBy<TU>(string key, TU value) {
            return _dao.GetBy(key, value);
        }

        public void Remove(T entity) {
            _dao.Delete(entity);
        }

        public void Remove(int id) {
            _dao.DeleteById(id);
        }
    }
}