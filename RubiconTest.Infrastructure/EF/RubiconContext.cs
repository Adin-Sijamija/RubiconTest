using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RubiconTest.Core.Entities;

namespace RubiconTest.Infrastructure.EF
{
    public class RubiconContext: DbContext
    {


        public RubiconContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = RubicoTestAdinSijamija; Integrated Security = True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>().HasKey(bg => new { bg.BlogSlug, bg.TagId});

            modelBuilder.Entity<Blog>().Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Blog>().Property(x => x.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }


    }
}
