using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataGrainEf.Example.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ExampleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExampleContext context)
        {
            DataGrain<ExampleContext>.Seed(context, this);
        }
    }
}
