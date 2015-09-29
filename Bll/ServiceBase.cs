using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YHCSheng.Dal;

namespace YHCSheng.Bll {
    public class ServiceBase<T> where T : class {
        private readonly IDao<T> _dao;

        protected ServiceBase(IDao<T> dao ) {
            _dao = dao;
        }

        public IList<T> GetByCondition(Expression<Func<T, bool>> conditions, Dictionary<string, bool> order) {
            return _dao.GetByCondition(conditions, order);
        }

        public IList<T> GetPageList(int pageSize, int pageIndex, out int recordCount, Expression<Func<T, bool>> where, Dictionary<string, bool> order) {
            return _dao.GetPageList(pageSize, pageIndex, out recordCount, where, order);
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

        public void Delete(T entity) {
            _dao.Delete(entity);
        }

        public void Delete(int id) {
            _dao.DeleteById(id);
        }
    }
}