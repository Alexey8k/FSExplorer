using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumLocalization.Extension
{
    public static class StringExtension
    {
        public static object ToEnum(this string obj, Type typeEnum)
        {
            return TypeDescriptor.GetConverter(typeEnum).ConvertFromString(obj);
        }
    }
}
