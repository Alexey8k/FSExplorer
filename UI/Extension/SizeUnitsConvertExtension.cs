using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Enum;

namespace UI.Extension
{
    public static class SizeUnitsConvertExtension
    {
        public static decimal ToSizeUnits(this long value, SizeUnits unit)
            => value / (decimal)Math.Pow(1024, (long)unit);
    }
}
