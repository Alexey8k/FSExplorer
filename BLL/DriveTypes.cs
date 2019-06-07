using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Flags]
    public enum DriveTypes
    {
        Unknown = 0x1,
        NoRootDir = 0x2,
        Removable = 0x4, 
        Fixed = 0x8, 
        Remote = 0x10, 
        CDRom = 0x20,
        RAMDisk = 0x40,
        All = Unknown | NoRootDir | Removable | Fixed | Remote | CDRom | RAMDisk
    }
}
