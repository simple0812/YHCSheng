using YHCSheng.Dal;
using YHCSheng.Models;

namespace YHCSheng.Bll {
    public class UserService : ServiceBase<User> {
        public UserService(IDao<User> dao ) : base(dao) {
            
        }
    }
}