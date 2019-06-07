using FSInfo;
using SHFileInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace FSIcon
{
    public class FSIconService : IFSIconService
    {
        public BitmapSource GetIcon(FSItemInfo itemInfo, bool isSmall = true)
        {
            var uFlags = SHGFI.Icon | (isSmall ? SHGFI.SmallIcon : SHGFI.LargeIcon);

            if (itemInfo.FileAttributes.HasFlag(FileAttributes.Directory))
            {
                if (itemInfo.Attributes.HasFlag(SFGAO.Link))
                    uFlags |= SHGFI.LinkOverlay;
            }
            else
                uFlags |= SHGFI.UseFileAttributes;


            using (var info = new SHFInfo())
            {
                ref readonly var shfl = ref info.GetSHFileInfo(itemInfo.Path, 0, uFlags);

                return Imaging.CreateBitmapSourceFromHIcon(
                    shfl.hIcon,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
        }

    }
}
