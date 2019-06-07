using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSExplorer
    {
        IEnumerable<FSItemModel> GetDrives(DriveTypes driveTypes);
        IEnumerable<FSItemModel> GetFSItems(string strPath);
        FSItemModel GetFSItem(string strPath);
        bool HasItems(string strPath);
        bool IsExistsItem(string strPath);
        DateTime GetLastWriteTime(string strPath);
    }
}
