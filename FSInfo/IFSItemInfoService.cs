using SHFileInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSInfo
{
    public interface IFSItemInfoService
    {
        FSItemInfo GetFSItemInfo(string strPath);
    }
}
