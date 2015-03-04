using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf.Config
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataMigrationAttribute : Attribute
    {
        private readonly Rule _rule;
        public Rule Rule { get { return _rule; } }

        private readonly Type _targetMigration;
        public Type TargetMigration { get { return _targetMigration; } }

        public DataMigrationAttribute(Rule rule, Type targetMigration)
        {
            _rule = rule;
            _targetMigration = targetMigration;
        }
    }
}
