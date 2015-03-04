using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf.Config
{
    public enum Rule
    {
        EverySeedAttempt,
        OncePerMigration,
        Once,
    }
}
