using DataGrainEf.Example.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf.Example
{
    public class ExampleContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ExampleContext()
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            DataGrain.AddTableToModel(modelBuilder);

            modelBuilder.Entity<Account>();
            modelBuilder.Entity<Order>();
        }
    }
}
