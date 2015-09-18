//用户
using System;
using YHCSheng.Utils;
using System.Reflection;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using System.Data;

namespace YHCSheng.Models {
	public class User {
	    public int Id { get; set; }
	    public string Name {get; set;}
	    public string Nick {get; set;}
	    public string Email {get; set;}
	    public string Portrait {get; set;}

	    [CustomJson(true)]
	    public string Password {get; set;}
	    public int Status {get;set;}
	    public int CreatedAt { get; set; }
	    [CustomJson("updateAt")]
	    public int UpdatedAt { get; set; }
	}
}