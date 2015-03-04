using System;
using System.Data.Entity;

using DataGrainEf.Config;

namespace DataGrainEf.Factory
{
    public class ActivatorMigrationFactory<TDbContext> : IMigrationFactory<TDbContext> where TDbContext : DbContext
    {
        public IDataMigration<TDbContext> Build(Type type) 
        {
            return Activator.CreateInstance(type) as IDataMigration<TDbContext>;
        }
    }
}
