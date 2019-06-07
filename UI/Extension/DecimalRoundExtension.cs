using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Extension
{
    public static class DecimalRoundExtension
    {
        public static decimal Round(this decimal value, int countNumderAfterZero)
            => Math.Round(value, CountZeroAfterComma(value) + countNumderAfterZero);

        private static int CountZeroAfterComma(decimal value)
            => value == 0 || (long)(value *= 10) > 0 ? 0 : CountZeroAfterComma(value) + 1;
    }
}
