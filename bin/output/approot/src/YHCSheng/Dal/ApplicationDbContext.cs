using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.Configuration;

using YHCSheng.Models;

namespace YHCSheng.Dal {
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
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}