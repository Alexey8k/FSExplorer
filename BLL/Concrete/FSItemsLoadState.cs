using BLL.Abstract;
using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BLL.Concrete
{
    internal sealed class FSItemsLoadState : FSItemLoaderState
    {
        public FSItemsLoadState(
            DateTime lastWriteTime,
            IFSExplorer explorer,
            IFSItemLoaderStateFactory loaderStateFactory)
            : base(explorer, loaderStateFactory)
            => _lastWriteTime = lastWriteTime;

        public sealed override void LoadItems(IFSItemLoader itemLoader)
        {
            IEnumerator<FSItemModel> enumerator = _explorer.GetFSItems(itemLoader.ItemPath).GetEnumerator();

            itemLoader.ItemCollection.Clear();

            itemLoader.ItemsLoaderState = AddItems(itemLoader, enumerator)
            ? _loaderStateFactory.CreateLoadingItemsState(enumerator, _explorer.GetLastWriteTime(itemLoader.ItemPath))
            : _loaderStateFactory.CreateLoadedItemsState(_explorer.GetLastWriteTime(itemLoader.ItemPath));
        }
    }
}
