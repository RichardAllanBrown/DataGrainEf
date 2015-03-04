using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using DataGrainEf.Example;

namespace DataGrainEf.ConsoleDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning Deployment...");

            try
            {
                DoIt();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR!");
                Console.WriteLine(ex);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        private static void DoIt()
        {
            var migrator = new DbMigrator(new DataGrainEf.Example.Migrations.Configuration());

            Console.WriteLine();
            Console.WriteLine("Migrations already applied:");
            foreach (var appliedMigration in migrator.GetDatabaseMigrations())
                Console.WriteLine(appliedMigration);

            Console.WriteLine();
            Console.WriteLine("Migrations to be applied:");
            foreach (var pending in migrator.GetPendingMigrations())
                Console.WriteLine(pending);

            Console.WriteLine();
            Console.WriteLine("Starting Migration...");
            migrator.Update();
            Console.WriteLine("Migration Complete");
        }
    }
}
