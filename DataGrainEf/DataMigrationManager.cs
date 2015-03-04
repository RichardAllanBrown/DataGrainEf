using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;

using DataGrainEf.Util;
using DataGrainEf.Config;
using DataGrainEf.Factory;

namespace DataGrainEf
{
    public interface IDataMigrationManager<T> where T : DbContext
    {
        void MigrateToLatest(T context, DbMigrationsConfiguration<T> migratorConfig);
        IEnumerable<DataMigration> GetAppliedDataMigrations(T context);
    }

    public class DataMigrationManager<T> : IDataMigrationManager<T> where T : DbContext
    {
        private readonly IMigrationFactory<T> _factory;

        public DataMigrationManager(IMigrationFactory<T> factory)
        {
            _factory = factory;
        }

        public void MigrateToLatest(T context, DbMigrationsConfiguration<T> migratorConfig)
        {
            var migrator = new DbMigrator(migratorConfig);

            var dataMigrationTypes = GetMigrationTypes();
            var latestAppliedMigration = GetLatestAppliedMigrationName(migrator);
            var appliedMigrations = GetAppliedDataMigrations(context);

            var migrationsToApply = dataMigrationTypes
                .Where(x => TargetsAppliedMigration(x, latestAppliedMigration) 
                    && !MigrationAlreadyApplied(x, appliedMigrations))
                .OrderBy(x => x.GetAttribute<DataMigrationAttribute>().TargetMigration.Name);

            ApplyMigrations(migrationsToApply, context);
        }

        private string GetLatestAppliedMigrationName(DbMigrator migrator)
        {
            return migrator.GetDatabaseMigrations().OrderBy(x => x).Last();
        }

        private bool TargetsAppliedMigration(Type type, string latestMigrationApplied)
        {
            return type.GetAttribute<DataMigrationAttribute>().TargetMigration.Name.CompareTo(latestMigrationApplied) < 0;
        }

        private IEnumerable<Type> GetMigrationTypes()
        {
            var dataMigrationClasses = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces().Contains(typeof(IDataMigration<T>))
                    && x.GetCustomAttributes(typeof(DataMigrationAttribute), true).Any());

            return dataMigrationClasses;
        }

        private bool MigrationAlreadyApplied(Type type, IEnumerable<DataMigration> appliedMigrations)
        {
            var attr = type.GetAttribute<DataMigrationAttribute>();
            switch (attr.Rule)
            {
                case Rule.Once:
                    return appliedMigrations.Any(x => x.MigrationName.Equals(type.FullName, StringComparison.InvariantCultureIgnoreCase));

                case Rule.EverySeedAttempt:
                    return false;

                case Rule.OncePerMigration:
                    return appliedMigrations.Any(x => x.MigrationName.Equals(type.FullName, StringComparison.InvariantCultureIgnoreCase)
                        && x.MigrationId == attr.TargetMigration.Name);

                default:
                    throw new ArgumentOutOfRangeException("attr.Rule");
            }
        }

        public IEnumerable<DataMigration> GetAppliedDataMigrations(T context)
        {
            return context.Set<DataMigration>().ToList();
        }

        private void ApplyMigrations(IOrderedEnumerable<Type> migrationsToApply, T context)
        {
            var migrationSet = context.Set<DataMigration>();

            foreach (var mig in migrationsToApply)
            {
                var attr = mig.GetCustomAttribute(typeof(DataMigrationAttribute), true) as DataMigrationAttribute;
                var instance = _factory.Build(mig);
                instance.MigrateUp(context);

                migrationSet.Add(new DataMigration()
                {
                    AppliedOn = DateTime.Now,
                    ContextKey = typeof(T).FullName,
                    MigrationName = mig.FullName,
                    MigrationId = attr.TargetMigration.FullName,
                });
            }

            context.SaveChanges();
        }
    }
}
