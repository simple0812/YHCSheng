using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;
using YHCSheng.Dal;

namespace YHCSheng.Migrations {
    [DbContext(typeof (ApplicationDbContext))]
    internal class ApplicationDbContextModelSnapshot : ModelSnapshot {
        protected override void BuildModel(ModelBuilder modelBuilder) {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("YHCSheng.Models.Article", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Content");

                b.Property<int>("CreatedAt");

                b.Property<int>("Status");

                b.Property<string>("Title");

                b.Property<int>("UpdatedAt");

                b.Property<int>("UserId");

                b.Key("Id");
            });

            modelBuilder.Entity("YHCSheng.Models.Attention", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("AttentionUserId");

                b.Property<int>("CreatedAt");

                b.Property<string>("Remark");

                b.Property<int>("Status");

                b.Property<int>("UpdatedAt");

                b.Property<int>("UserId");

                b.Key("Id");
            });

            modelBuilder.Entity("YHCSheng.Models.Collection", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("ArticleId");

                b.Property<int>("CreatedAt");

                b.Property<string>("Remark");

                b.Property<int>("Status");

                b.Property<int>("UpdatedAt");

                b.Property<int>("UserId");

                b.Key("Id");
            });

            modelBuilder.Entity("YHCSheng.Models.Comment", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("ArticleId");

                b.Property<int>("CreatedAt");

                b.Property<string>("Remark");

                b.Property<int>("Status");

                b.Property<int>("UpdatedAt");

                b.Property<int>("UserId");

                b.Key("Id");
            });

            modelBuilder.Entity("YHCSheng.Models.User", b => {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

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