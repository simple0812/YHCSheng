using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;

using YHCSheng.Dal;
using YHCSheng.Utils;
using YHCSheng.Models;

namespace YHCSheng.Bll {
    public class UserService {
        private IDao<User> Dao;
        
        public UserService() {
            Dao = new UserDao();
        }

        public IList<User> Retrieve(Dictionary<string, object> conditions = null) {
            return Dao.Find(conditions);
        }

        public User Create(User user) {
            return Dao.Insert(user);
        }

        public User Update(User user) {
            return Dao.Update(user);
        }

        public User GetById(int id) {
            return Dao.GetById(id);
        }

        public User GetBy<U>(string key, U value) {
            return Dao.GetBy(key, value);
        }

        public void Remove(User user) {
            Dao.Delete(user);
        }

        public void Remove(int id) {
            var user = Dao.GetById(id);
            if(user != null)
                Dao.Delete(Dao.GetById(id));
        }
    }
}