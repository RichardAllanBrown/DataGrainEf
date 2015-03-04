
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf.Config
{
    public interface IDataMigration<T> where T : DbContext
    {
        void MigrateUp(T context);
    }
}
