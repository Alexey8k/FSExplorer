using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class StrCmpLogicalComparer
    {
        public static Comparer<string> Create => Comparer<string>.Create((x, y) => StrCmpLogicalW(x, y));

        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);
    }
}
