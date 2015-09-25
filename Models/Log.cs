//日志
namespace YHCSheng.Models {
    public class Log {
        public int Id { get; set; }
        public string Content {get; set;}
        public int Status {get; set;}
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
    }
}