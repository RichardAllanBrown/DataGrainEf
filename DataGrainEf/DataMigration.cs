using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf
{
    public class DataMigration
    {
        public int Id { get; set; }

        public string MigrationId { get; set; }

        public string ContextKey { get; set; }

        public string MigrationName { get; set; }

        public DateTime AppliedOn { get; set; }
    }
}
