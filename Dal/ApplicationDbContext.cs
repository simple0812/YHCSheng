using Microsoft.Data.Entity;

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

        protected override void OnConfiguring(EntityOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(GlobalVariables.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}