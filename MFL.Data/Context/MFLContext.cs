using MFL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;

namespace MFL.Data.Context
{
    public class MFLContext : DbContext
    {
        public MFLContext(DbContextOptions<MFLContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MFLContext).Assembly);
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;

            foreach (var entity in ChangeTracker.Entries())
            {
                if (entity.Entity is IUpdatable updatable)
                {
                    if (entity.State == EntityState.Added)
                    {
                        updatable.CreatedDate = now;
                        updatable.UpdatedDate = now;
                    }
                    else if (entity.State == EntityState.Modified)
                    {
                        updatable.UpdatedDate = now;
                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<WaiverTransaction> WaiverTransactions { get; set; }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MFLContext>
    {
        public MFLContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../MFL/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<MFLContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseMySQL(connectionString);
            return new MFLContext(builder.Options);
        }
    }
}
