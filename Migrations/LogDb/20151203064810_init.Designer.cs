using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using YHCSheng.Dal;

namespace YHCSheng.Migrations.LogDb
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20151203064810_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YHCSheng.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("CreatedAt");

                    b.Property<int>("Status");

                    b.Property<int>("UpdatedAt");

                    b.HasKey("Id");
                });
        }
    }
}
