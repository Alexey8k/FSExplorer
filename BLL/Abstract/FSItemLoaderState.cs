using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BLL.Abstract
{
    public abstract class FSItemLoaderState
    {
        protected IFSExplorer _explorer;
        protected IFSItemLoaderStateFactory _loaderStateFactory;
        protected DateTime _lastWriteTime;

        public FSItemLoaderState(IFSExplorer explorer, IFSItemLoaderStateFactory loaderStateFactory)
        {
            _explorer = explorer;
            _loaderStateFactory = loaderStateFactory;
        }

        public abstract void LoadItems(IFSItemLoader itemLoader);

        public void ValidateItemState(IFSItemLoader itemsLoader)
        {
            if (IsValidCurrentItem(itemsLoader.ItemPath)) return;

            itemsLoader.ItemsLoaderState = _loaderStateFactory.CreatePreLoadItemsState();
            itemsLoader.BeginLoadItems();
        }

        protected bool AddItems(IFSItemLoader itemLoader, IEnumerator<FSItemModel> enumerator)
        {
            var hasNext = enumerator.MoveNext();
            if (!hasNext) return hasNext;
            do
            {
                itemLoader.ItemCollection.Add(enumerator.Current);
            } while (/*itemLoader.IsLoadingItems && */(hasNext = enumerator.MoveNext()));

            return hasNext;
        }

        protected void ValideteChildItemsStete(IFSItemLoader itemsLoader)
        {
            foreach (var itemManager in itemsLoader.ItemCollection.ItemManagers)
                if (itemsLoader.IsLoadingItems)
                    itemManager.ValidateItemState();
        }

        private bool IsValidCurrentItem(string strPath)
            => _lastWriteTime == _explorer.GetLastWriteTime(strPath);
    }
}
