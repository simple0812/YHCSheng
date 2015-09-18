using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace YHCSheng.Migrations
{
    public partial class init : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 1);
            migration.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    Content = table.Column(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column(type: "int", nullable: false),
                    Status = table.Column(type: "int", nullable: false),
                    Title = table.Column(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column(type: "int", nullable: false),
                    UserId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });
            migration.CreateTable(
                name: "Attention",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    AttentionUserId = table.Column(type: "int", nullable: false),
                    CreatedAt = table.Column(type: "int", nullable: false),
                    Remark = table.Column(type: "nvarchar(max)", nullable: true),
                    Status = table.Column(type: "int", nullable: false),
                    UpdatedAt = table.Column(type: "int", nullable: false),
                    UserId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attention", x => x.Id);
                });
            migration.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    ArticleId = table.Column(type: "int", nullable: false),
                    CreatedAt = table.Column(type: "int", nullable: false),
                    Remark = table.Column(type: "nvarchar(max)", nullable: true),
                    Status = table.Column(type: "int", nullable: false),
                    UpdatedAt = table.Column(type: "int", nullable: false),
                    UserId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });
            migration.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    ArticleId = table.Column(type: "int", nullable: false),
                    CreatedAt = table.Column(type: "int", nullable: false),
                    Remark = table.Column(type: "nvarchar(max)", nullable: true),
                    Status = table.Column(type: "int", nullable: false),
                    UpdatedAt = table.Column(type: "int", nullable: false),
                    UserId = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                });
            migration.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column(type: "int", nullable: false),
                    CreatedAt = table.Column(type: "int", nullable: false),
                    Email = table.Column(type: "nvarchar(max)", nullable: true),
                    Name = table.Column(type: "nvarchar(max)", nullable: true),
                    Nick = table.Column(type: "nvarchar(max)", nullable: true),
                    Password = table.Column(type: "nvarchar(max)", nullable: true),
                    Portrait = table.Column(type: "nvarchar(max)", nullable: true),
                    Status = table.Column(type: "int", nullable: false),
                    UpdatedAt = table.Column(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("Article");
            migration.DropTable("Attention");
            migration.DropTable("Collection");
            migration.DropTable("Comment");
            migration.DropTable("User");
        }
    }
}
