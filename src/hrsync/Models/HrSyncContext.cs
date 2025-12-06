using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using hrsync.Models.Configuration;
using hrsync.Models;

namespace hrsync.Models
{
    public class HrSyncContext : DbContext
    {
        public DbSet<HrmasterdataOu> HrmasterdataOu { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<HrSchedulerConfiguration> HrSchedulerConfiguration { get; set; }
        public DbSet<Email> Email {get; set;}

        private readonly IOptions<DatabaseConfiguration> datasource;

        public HrSyncContext(IOptions<DatabaseConfiguration> datasource)
        {
            this.datasource = datasource;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(string.Format("{0}", datasource.Value.ConnectionString));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(datasource.Value.ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(datasource.Value.Schema);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.Relational().TableName.ToLower();
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    string str = property.Relational().ColumnName;
                    property.Relational().ColumnName = System.Text.RegularExpressions.Regex.Replace(str, "(?<=.)([A-Z])", "_$0",
                        System.Text.RegularExpressions.RegexOptions.Compiled).ToLower();
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    foreach (var fk in entityType.FindForeignKeys(property))
                    {
                        string str = fk.Relational().Name;
                        fk.Relational().Name = System.Text.RegularExpressions.Regex.Replace(str, "(?<=.)([A-Z])", "_$0",
                        System.Text.RegularExpressions.RegexOptions.Compiled).ToLower();
                    }
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var index in entityType.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name;
                }
            }
        }
    }
}