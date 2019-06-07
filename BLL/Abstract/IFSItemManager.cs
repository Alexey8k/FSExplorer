using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSItemManager
    {
        event EventHandler UpdateLoaderState;
        IFSItemCollection ItemCollection { get; set; }
        bool IsManagement { get; }
        void BeginManagement();
        void EndManagement();
        void ValidateItemState();
    }
}
