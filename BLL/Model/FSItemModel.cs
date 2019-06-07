using FSInfo;
using SHFileInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL.Model
{
    public class FSItemModel : IComparable<FSItemModel>
    {
        private FSItemInfo _itemInfo;
        private Lazy<BitmapSource> _largeIconLazy;

        public FSItemModel(FSItemInfo itemInfo, BitmapSource smallIconLazy, Lazy<BitmapSource> largeIconLazy)
        {
            _itemInfo = itemInfo;
            SmallIcon = smallIconLazy;
            _largeIconLazy = largeIconLazy;
        }

        public string DisplayName => _itemInfo.DisplayName;
        public string TypeName => _itemInfo.TypeName;
        public bool IsNode => _itemInfo.FileAttributes.HasFlag(FileAttributes.Directory);
        public string Path => _itemInfo.Path;
        public BitmapSource SmallIcon { get; }
        public BitmapSource LargeIcon => _largeIconLazy.Value;

        #region IComparable

        public int CompareTo(FSItemModel other)
            => IsNode.CompareTo(other.IsNode) != 0
            ? -IsNode.CompareTo(other.IsNode)
            : StrCmpLogicalComparer.Create.Compare(DisplayName, other.DisplayName);

        #endregion // IComparable

    }
}
