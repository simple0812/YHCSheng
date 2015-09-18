using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;
namespace YHCSheng.Test {
    public class CustomerService : IMyService {
        public CustomerService(LoggingService myServiceInstance) {
            myServiceInstance.WriteToLog("SomeValue");
    　　}
    }

    public class LoggingService {
        public void WriteToLog(string msg) {
            Console.WriteLine(msg);        
        }
    }

    public interface IMyService {

    }


    public class TestService {
        private ITodoRepository _repository;
        public TestService(ITodoRepository r) {
            _repository = r;
        }

        public void Show() {
            Console.WriteLine("xxxxxxxxxx");
        }
    }

    public interface ITodoRepository {
        IEnumerable<TodoItem> AllItems { get; }
        void Add(TodoItem item);
        TodoItem GetById(int id);
        bool TryDelete(int id);
    }

    public class TodoItem {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TodoRepository : ITodoRepository {
        readonly List<TodoItem> _items = new List<TodoItem>();

        public IEnumerable<TodoItem> AllItems {
            get { return _items; }
        }

        public TodoItem GetById(int id) {
            return _items.FirstOrDefault(x => x.Id == id);
        }

        public void Add(TodoItem item) {
            item.Id = 1 + _items.Max(x => (int?)x.Id) ?? 0;
            _items.Add(item);
        }

        public bool TryDelete(int id) {
            var item = GetById(id);

            if (item == null) { return false; }

            _items.Remove(item);

            return true;
        }
    }
}
