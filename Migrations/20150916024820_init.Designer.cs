using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using YHCSheng.Dal;

namespace YHCSheng.Migrations
{
    [ContextType(typeof(ApplicationDbContext))]
    partial class init
    {
        public override string Id
        {
            get { return "20150916024820_init"; }
        }
        
        public override string ProductVersion
        {
            get { return "7.0.0-beta5-13549"; }
        }
        
        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("SqlServer:DefaultSequenceName", "DefaultSequence")
                .Annotation("SqlServer:Sequence:.DefaultSequence", "'DefaultSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .Annotation("SqlServer:ValueGeneration", "Sequence");
            
            builder.Entity("YHCSheng.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<string>("Content");
                    
                    b.Property<int>("CreatedAt");
                    
                    b.Property<int>("Status");
                    
                    b.Property<string>("Title");
                    
                    b.Property<int>("UpdatedAt");
                    
                    b.Property<int>("UserId");
                    
                    b.Key("Id");
                });
            
            builder.Entity("YHCSheng.Models.Attention", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<int>("AttentionUserId");
                    
                    b.Property<int>("CreatedAt");
                    
                    b.Property<string>("Remark");
                    
                    b.Property<int>("Status");
                    
                    b.Property<int>("UpdatedAt");
                    
                    b.Property<int>("UserId");
                    
                    b.Key("Id");
                });
            
            builder.Entity("YHCSheng.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<int>("ArticleId");
                    
                    b.Property<int>("CreatedAt");
                    
                    b.Property<string>("Remark");
                    
                    b.Property<int>("Status");
                    
                    b.Property<int>("UpdatedAt");
                    
                    b.Property<int>("UserId");
                    
                    b.Key("Id");
                });
            
            builder.Entity("YHCSheng.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<int>("ArticleId");
                    
                    b.Property<int>("CreatedAt");
                    
                    b.Property<string>("Remark");
                    
                    b.Property<int>("Status");
                    
                    b.Property<int>("UpdatedAt");
                    
                    b.Property<int>("UserId");
                    
                    b.Key("Id");
                });
            
            builder.Entity("YHCSheng.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .GenerateValueOnAdd()
                        .StoreGeneratedPattern(StoreGeneratedPattern.Identity);
                    
                    b.Property<int>("CreatedAt");
                    
                    b.Property<string>("Email");
                    
                    b.Property<string>("Name");
                    
                    b.Property<string>("Nick");
                    
                    b.Property<string>("Password");
                    
                    b.Property<string>("Portrait");
                    
                    b.Property<int>("Status");
                    
                    b.Property<int>("UpdatedAt");
                    
                    b.Key("Id");
                });
        }
    }
}
