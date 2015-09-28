using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace YHCSheng.Migrations {
    public partial class init : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable("Article", table => new {
                Id = table.Column<int>(isNullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                Content = table.Column<string>(isNullable: true),
                CreatedAt = table.Column<int>(isNullable: false),
                Status = table.Column<int>(isNullable: false),
                Title = table.Column<string>(isNullable: true),
                UpdatedAt = table.Column<int>(isNullable: false),
                UserId = table.Column<int>(isNullable: false)
            },
                constraints: table => { table.PrimaryKey("PK_Article", x => x.Id); });
            migrationBuilder.CreateTable("Attention", table => new {
                Id = table.Column<int>(isNullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                AttentionUserId = table.Column<int>(isNullable: false),
                CreatedAt = table.Column<int>(isNullable: false),
                Remark = table.Column<string>(isNullable: true),
                Status = table.Column<int>(isNullable: false),
                UpdatedAt = table.Column<int>(isNullable: false),
                UserId = table.Column<int>(isNullable: false)
            },
                constraints: table => { table.PrimaryKey("PK_Attention", x => x.Id); });
            migrationBuilder.CreateTable("Collection", table => new {
                Id = table.Column<int>(isNullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                ArticleId = table.Column<int>(isNullable: false),
                CreatedAt = table.Column<int>(isNullable: false),
                Remark = table.Column<string>(isNullable: true),
                Status = table.Column<int>(isNullable: false),
                UpdatedAt = table.Column<int>(isNullable: false),
                UserId = table.Column<int>(isNullable: false)
            },
                constraints: table => { table.PrimaryKey("PK_Collection", x => x.Id); });
            migrationBuilder.CreateTable("Comment", table => new {
                Id = table.Column<int>(isNullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                ArticleId = table.Column<int>(isNullable: false),
                CreatedAt = table.Column<int>(isNullable: false),
                Remark = table.Column<string>(isNullable: true),
                Status = table.Column<int>(isNullable: false),
                UpdatedAt = table.Column<int>(isNullable: false),
                UserId = table.Column<int>(isNullable: false)
            },
                constraints: table => { table.PrimaryKey("PK_Comment", x => x.Id); });
            migrationBuilder.CreateTable("User", table => new {
                Id = table.Column<int>(isNullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                CreatedAt = table.Column<int>(isNullable: false),
                Email = table.Column<string>(isNullable: true),
                Name = table.Column<string>(isNullable: true),
                Nick = table.Column<string>(isNullable: true),
                Password = table.Column<string>(isNullable: true),
                Portrait = table.Column<string>(isNullable: true),
                Status = table.Column<int>(isNullable: false),
                UpdatedAt = table.Column<int>(isNullable: false)
            },
                constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable("Article");
            migrationBuilder.DropTable("Attention");
            migrationBuilder.DropTable("Collection");
            migrationBuilder.DropTable("Comment");
            migrationBuilder.DropTable("User");
        }
    }
}