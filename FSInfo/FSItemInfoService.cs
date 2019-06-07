using SHFileInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FSInfo
{
    public class FSItemInfoService : IFSItemInfoService
    {
        public FSItemInfo GetFSItemInfo(string strPath)
        {
            var fileAttributes = File.GetAttributes(strPath);
            var uFlags = SHGFI.Icon | SHGFI.DisplayName | SHGFI.TypeName;

            uFlags |= fileAttributes.HasFlag(FileAttributes.Directory) ? SHGFI.Attributes | SHGFI.OverlayIndex : SHGFI.UseFileAttributes;

            using (var shfi = new SHFInfo())
            {
                ref readonly var fsii = ref shfi.GetSHFileInfo(strPath, 0, uFlags);
                return new FSItemInfo
                {
                    Path = strPath,
                    DisplayName = fsii.szDisplayName,
                    TypeName = fsii.szTypeName,
                    FileAttributes = fileAttributes,
                    Attributes = (SFGAO)fsii.dwAttributes,
                    IconIndex = fsii.iIcon
                };
            }
        }
    }
}
