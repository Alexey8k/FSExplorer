using FSIcon;
using FSInfo;
using SHFileInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL
{
    public class FSIconServiceProxy : IFSIconService
    {
        private object _lock = new object();
        private IFSIconService _iconService;

        private SortedDictionary<int, BitmapSource> _cashSmallIcons = new SortedDictionary<int, BitmapSource>();
        private SortedDictionary<int, BitmapSource> _cashLargeIcons = new SortedDictionary<int, BitmapSource>();

        public FSIconServiceProxy(IFSIconService iconService)
        {
            _iconService = iconService;
        }

        public BitmapSource GetIcon(FSItemInfo itemInfo, bool isSmall = true)
        {
            lock (_lock)
            {
                var cashIcons = isSmall ? _cashSmallIcons : _cashLargeIcons;
                if (cashIcons.TryGetValue(itemInfo.IconIndex, out BitmapSource icon))
                    return icon;
                var newIcon = GetIconFreeze(itemInfo, isSmall);
                cashIcons.Add(itemInfo.IconIndex, newIcon);
                return newIcon;
            }
        }

        private BitmapSource GetIconFreeze(FSItemInfo itemInfo, bool isSmall)
        {
            var icon = _iconService.GetIcon(itemInfo, isSmall);
            icon.Freeze();
            return icon;
        }
    }
}
