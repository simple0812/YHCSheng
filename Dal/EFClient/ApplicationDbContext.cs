using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;

using YHCSheng.Models;

namespace YHCSheng.Dal
{
    public class ApplicationDbContext : DbContext {
        public DbSet<Article> Articles {get;set;}
        public DbSet<Attention> Attentions {get;set;}
        public DbSet<Collection> Collections {get;set;}
        public DbSet<Comment> Comments {get;set;}
        public DbSet<User> Users {get;set;}
        
        public ApplicationDbContext() {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(GlobalVariables.DefaultConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}