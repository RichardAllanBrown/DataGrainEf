using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using DataGrainEf;
using DataGrainEf.Config;
using DataGrainEf.Example.Migrations;
using DataGrainEf.Example.Model;

namespace DataGrainEf.Example.DataMigrations
{
    [DataMigration(Rule.Once, typeof(InitialMigration))]
    public class BasicRecordInsertion : IDataMigration<ExampleContext>
    {
        public void MigrateUp(ExampleContext context)
        {
            context.Accounts.Add(new Account()
            {
                CreatedOn = DateTime.Now.AddDays(-4),
                Name = "Test Acc",
                Orders = new List<Order>()
                {
                    new Order() { CreatedOn = DateTime.Now.AddDays(-3), Total = 21.34m },
                    new Order() { CreatedOn = DateTime.Now.AddDays(-1), Total = 12.58m },
                }
            });
        }
    }
}
