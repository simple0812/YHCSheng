using Microsoft.Data.Entity;

using YHCSheng.Models;

namespace YHCSheng.Dal
{
    public class LogDbContext : DbContext {
        public DbSet<Log> Logs {get;set;}
        
        public LogDbContext() {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(GlobalVariables.LogConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}