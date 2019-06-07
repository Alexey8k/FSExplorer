using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.EnumConverter;

namespace UI.Enum
{
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum SizeUnits
    {
        Byte,
        Kilobyte,
        Megabyte,
        Gigabyte
    }
}
