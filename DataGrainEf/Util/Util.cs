using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGrainEf.Util
{
    internal static class EnumUtil
    {
        internal static T GetAttribute<T>(this Enum enumVal)
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), true);

            if (0 < attributes.Length)
                return (T)attributes[0];

            throw new ArgumentOutOfRangeException("T");
        }
    }

    internal static class TypeUtil
    {
        internal static T GetAttribute<T>(this Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(T), true);

            if (0 < attributes.Length)
                return (T)attributes[0];

            throw new ArgumentOutOfRangeException("T");
        }
    }
}
