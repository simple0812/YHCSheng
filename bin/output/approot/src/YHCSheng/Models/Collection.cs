//用户收藏的文章
namespace YHCSheng.Models {
	public class Collection {
	    public int Id { get; set; }
	    public int UserId { get; set; }
	    public int ArticleId { get; set; }
	    public string Remark {get; set;}
	    public int Status {get;set;}
	    public int CreatedAt { get; set; }
	    public int UpdatedAt { get; set; }
	}
}