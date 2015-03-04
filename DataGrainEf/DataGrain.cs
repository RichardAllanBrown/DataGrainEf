using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataGrainEf.Factory;

namespace DataGrainEf
{
    public static class DataGrain<T> where T : DbContext
    {
        public static void Seed(T context, DbMigrationsConfiguration<T> migrator, IMigrationFactory<T> migrationFactory)
        {
            var migrationManager = new DataMigrationManager<T>(migrationFactory);
            migrationManager.MigrateToLatest(context, migrator);
        }

        public static void Seed(T context, DbMigrationsConfiguration<T> migrator) 
        {
            var migrationFactory = new ActivatorMigrationFactory<T>();
            Seed(context, migrator, migrationFactory);
        }
    }

    public static class DataGrain
    {
        public static void AddTableToModel(DbModelBuilder modelBuilder)
        {
            var configger = modelBuilder.Entity<DataMigration>();

            configger.HasKey(k => k.Id);
            configger.ToTable("__DataMigration");

            configger.Property(p => p.AppliedOn).HasColumnName("AppliedOn");
            configger.Property(p => p.ContextKey).HasColumnName("ContextKey");
            configger.Property(p => p.MigrationId).HasColumnName("MigrationId");
            configger.Property(p => p.MigrationName).HasColumnName("MigrationName");
        }
    }
}
