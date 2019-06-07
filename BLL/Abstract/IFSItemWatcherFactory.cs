using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSItemWatcherFactory
    {
        IFSItemWatcher Create(string itemPath, IFSItemCollection itemCollection);
    }
}
