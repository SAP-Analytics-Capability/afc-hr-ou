using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using bwsync.Models.Configuration;

namespace bwsync.Models
{
    //per generare tabelle da terminale
    //1 dotnet ef migrations add InitialCreate
    //2 dotnet ef database update
    public class BwContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Email> Email {get; set;}

        private readonly IOptions<DatabaseConfiguration> datasource;

        public BwContext(IOptions<DatabaseConfiguration> datasource)
        {
            this.datasource = datasource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(datasource.Value.ConnectionString);
        //=> optionsBuilder.UseNpgsql("Host=localhost;Database=Test2;Username=postgres;Password=");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(datasource.Value.Schema);

            // Nome tabella lower da nome classe
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Add NuGet package "Humanizer" to use Singularize()
                entityType.Relational().TableName = entityType.Relational().TableName.ToLower();
            }

            //nome colonna in snake_case
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    string str = property.Relational().ColumnName;
                    property.Relational().ColumnName = System.Text.RegularExpressions.Regex.Replace(str, "(?<=.)([A-Z])", "_$0",
                        System.Text.RegularExpressions.RegexOptions.Compiled).ToLower();
                }
            }

            //nome foreign key in snake_case
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

            // Rename Indices
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