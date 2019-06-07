using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IFSItemLoaderStateFactory
    {
        FSItemLoaderState CreateLoadItemsState(DateTime lastWriteTime);
        FSItemLoaderState CreateLoadingItemsState(IEnumerator<FSItemModel> enumerator, DateTime lastWriteTime);
        FSItemLoaderState CreateLoadedItemsState(DateTime lastWriteTime);
        FSItemLoaderState CreatePreLoadItemsState();
    }
}
