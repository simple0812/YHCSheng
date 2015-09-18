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

        public IList<User> GetList() {
            return Dao.FindAll();
        }
    }
}