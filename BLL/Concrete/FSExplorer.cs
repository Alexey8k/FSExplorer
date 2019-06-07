using BLL.Abstract;
using BLL.Model;
using FSIcon;
using FSInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BLL.Concrete
{
    public class FSExplorer : IFSExplorer
    {
        #region Data

        private IFSIconService _iconServiceProxy;
        private IFSDriveService _driveService;
        private IFSItemInfoService _infoService;

        #endregion // Data

        #region Constructor

        public FSExplorer(IFSIconService iconServiceProxy, IFSDriveService driveService, IFSItemInfoService infoService)
        {
            _iconServiceProxy = iconServiceProxy;
            _driveService = driveService;
            _infoService = infoService;
        }

        #endregion // Constructor

        #region IFSExplorer 

        public IEnumerable<FSItemModel> GetDrives(DriveTypes driveTypes) 
            => _driveService.GetDrives(driveTypes).Select(GetFSItemModel);

        public IEnumerable<FSItemModel> GetFSItems(string strPath) 
            => Directory.EnumerateFileSystemEntries(strPath).Select(GetFSItemModel);

        public FSItemModel GetFSItem(string strPath) => GetFSItemModel(strPath);

        public bool HasItems(string strPath)
        {
            try
            {
                return Directory.EnumerateFileSystemEntries(strPath).Any();
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        public bool IsExistsItem(string strPath) => Directory.Exists(strPath) || File.Exists(strPath);

        public DateTime GetLastWriteTime(string strPath) => Directory.GetLastWriteTime(strPath);

        #endregion // IFSExplorer
        
        private BitmapSource GetIcon(FSItemInfo itemInfo, bool isSmall = true) 
            => _iconServiceProxy.GetIcon(itemInfo, isSmall);

        private FSItemModel GetFSItemModel(string strPath)
        {
            var itemInfo = _infoService.GetFSItemInfo(strPath);
            return new FSItemModel(itemInfo, GetIcon(itemInfo), GetIconLazy(itemInfo, false));
        }

        private Lazy<BitmapSource> GetIconLazy(FSItemInfo itemInfo, bool isSmall = true) 
            => new Lazy<BitmapSource>(() => GetIcon(itemInfo, isSmall));
        
    }
}
