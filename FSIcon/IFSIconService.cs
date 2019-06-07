using FSInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FSIcon
{
    public interface IFSIconService
    {
        BitmapSource GetIcon(FSItemInfo itemInfo, bool isSmall = true);
    }
}
