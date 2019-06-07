using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumLocalization.Extension
{
    public static class EnumExtension
    {
        public static string ToLocalazeString(this Enum obj)
        {
            return TypeDescriptor.GetConverter(obj.GetType()).ConvertToString(obj);
        }
    }
}
