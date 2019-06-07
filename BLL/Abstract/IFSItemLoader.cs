using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSItemLoader
    {
        event EventHandler UpdateLoaderState;
        string ItemPath { get; }
        bool IsLoadingItems { get; }
        bool IsLoadedItems { get; }
        FSItemLoaderState ItemsLoaderState { get; set; }
        IFSItemCollection ItemCollection { get; set; }
        void BeginLoadItems();
        void EndLoadItems();
        void ValidateItemState();
    }
}
