//文章
namespace YHCSheng.Models {
	public class Article {
	    public int Id { get; set; }
	    public string Title { get; set; }
	    public string Content { get; set; }
	    public int Status {get;set;}
	    public int CreatedAt { get; set; }
	    public int UpdatedAt { get; set; }
	    public int UserId {get; set;}
	}
}