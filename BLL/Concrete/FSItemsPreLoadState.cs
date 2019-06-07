using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BLL.Abstract;

namespace BLL.Concrete
{
    internal sealed class FSItemsPreLoadState : FSItemLoaderState
    {
        public FSItemsPreLoadState(
            IFSExplorer explorer,
            IFSItemLoaderStateFactory loaderStateFactory)
            : base(explorer, loaderStateFactory) { }

        public sealed override void LoadItems(IFSItemLoader itemLoader)
        {
            itemLoader.ItemCollection.Clear();
            if (_explorer.HasItems(itemLoader.ItemPath))
            {
                itemLoader.ItemCollection.Add(null);
                itemLoader.ItemsLoaderState = _loaderStateFactory.CreateLoadItemsState(_explorer.GetLastWriteTime(itemLoader.ItemPath));
            }
            else
                itemLoader.ItemsLoaderState = _loaderStateFactory.CreateLoadedItemsState(_explorer.GetLastWriteTime(itemLoader.ItemPath));
        }
    }
}
