using Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devs.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(p =>
            {
                p.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.Name).HasColumnName("Name");

                p.HasMany(p => p.Technologies);
            });

            ProgrammingLanguage[] programmingLanguageEntitySeeds = { new(1, "C#"), new(2, "Go"), new(3, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);

            modelBuilder.Entity<Technology>(t =>
            {
                t.ToTable("Technologies").HasKey(k => k.Id);
                t.Property(t => t.Id).HasColumnName("Id");
                t.Property(t => t.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                t.Property(t => t.Name).HasColumnName("Name");

                t.HasOne(t => t.ProgrammingLanguage);
            });

            Technology[] technologyEntitySeeds = { new(1,2,"Spring"),new(2,2,"JSP") };
            modelBuilder.Entity<Technology>().HasData(technologyEntitySeeds);
        }
    }
}
