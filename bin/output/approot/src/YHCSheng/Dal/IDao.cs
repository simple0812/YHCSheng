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

namespace YHCSheng.Dal {
    public interface IDao<T> where T : class {
        T Update(T entity);
        T Insert(T entity);
        void Delete(T entity);
        List<T> FindAll();
    }
}