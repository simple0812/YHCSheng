//用户关注的用户
namespace YHCSheng.Models {
	public class Attention {
	    public int Id { get; set; }
	    public int UserId { get; set; }
	    public int AttentionUserId { get; set; }
	    public string Remark {get; set;}
	    public int Status {get;set;}
	    public int CreatedAt { get; set; }
	    public int UpdatedAt { get; set; }
	}
}