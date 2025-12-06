using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using masterdata.Models.Configuration;
using masterdata.Models.HrSyncResult;
using masterdata.Models.SnowCostCenterResults;
using masterdata.Models;

namespace masterdata.Models
{
    public class MasterDataContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<HrmasterdataOu> HrmasterdataOu { get; set; }
        public DbSet<HrMasterdataOuBCK> HrmasterdataOuBCK { get; set; }
        public DbSet<CleanHrOU> CleanHrOU { get; set; }
        public DbSet<HrOu> HrOu { get; set; }
        public DbSet<HrOuBCK> HrOuBCK { get; set; }
        public DbSet<ClientAccess> ClientAccess { get; set; }
        public DbSet<AssociationCostCenter> AssociationCostCenter { get; set; }
        public DbSet<AssociationOrganizationUnit> AssociationOrganizationUnit { get; set; }
        public DbSet<SchedulerConfiguration> SchedulerConfiguration { get; set; }
        public DbSet<BwCC> BwCc { get; set; }
        public DbSet<BwCCBCK> BwCcBCK { get; set; }
        public DbSet<CleanBwCC> CleanBwCC { get; set; }
        public DbSet<FunctionalAck> FunctionalAck { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<TypologyTranscoding> TypologyTranscoding { get; set; }

        private readonly IOptions<DatabaseConfiguration> datasource;

        public MasterDataContext(IOptions<DatabaseConfiguration> datasource)
        {
            this.datasource = datasource;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(datasource.Value.ConnectionString);



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

            modelBuilder.HasSequence<int>("fictcode_seq");
        }

    }
}