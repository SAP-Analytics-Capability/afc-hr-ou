using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using rulesmngt.Models.Configuration;

namespace rulesmngt.Models
{
    //per generare tabelle da terminale
    //1 dotnet ef migrations add InitialCreate
    //2 dotnet ef database update
    public class RulesContext : DbContext
    {
        public DbSet<ActivityList> ActivityList { get; set; }
        public DbSet<ActivityMapping> ActivityMapping { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<Bpc> Bpc { get; set; }
        public DbSet<BusinessLines> BusinessLines { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<CompanyRules> CompanyRules { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Entity> Entity { get; set; }
        public DbSet<MacroOrg1> MacroOrg1 { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Perimeter> Perimeter { get; set; }
        public DbSet<Vcs> Vcs { get; set; }
        public DbSet<PerimetroConsolidamento> PerimetroConsolidamento { get; set; }
        public DbSet<ActivityAssociation> ActivityAssociation { get; set; }
        public DbSet<CompanyScope> CompanyScope { get; set; }
        public DbSet<Responsability> Responsability { get; set; }
        public DbSet<ItaGlo> ItaGlo { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<ExceptionTable> ExceptionTable { get; set; }
        public DbSet<ConstantValue> ConstantValue { get; set; }
        public DbSet<ClientAccess> ClientAccess { get; set; }
        public DbSet<SchedulerConfiguration> SchedulerConfiguration { get; set; }

        private readonly IOptions<DatabaseConfiguration> datasource;

        public RulesContext(IOptions<DatabaseConfiguration> datasource)
        {
            this.datasource = datasource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(datasource.Value.ConnectionString);
        //=> optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");

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