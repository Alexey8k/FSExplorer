using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSItemCollection
    {
        void Add(FSItemModel itemViewModel);
        bool Remove(string path);
        int Count { get; }
        IFSItemManager this[string itemPath] { get; }
        IEnumerable<IFSItemManager> ItemManagers { get; }
        void Clear();
        void ThreadSafeAction(Action<IEnumerable<IFSItem>> action);
    }
}
