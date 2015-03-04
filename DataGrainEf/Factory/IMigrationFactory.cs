using System;

using DataGrainEf.Config;
using System.Data.Entity;

namespace DataGrainEf.Factory
{
    public interface IMigrationFactory<TDbContext> where TDbContext : DbContext
    {
        IDataMigration<TDbContext> Build(Type type);
    }
}
