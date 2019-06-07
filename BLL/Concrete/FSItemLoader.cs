using BLL.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BLL.Concrete
{
    internal class FSItemLoader : IFSItemLoader
    {
        private FSItemLoaderState _itemsLoaderState;
        public FSItemLoader(FSItemLoaderState itemsLoaderState, string itemPath, IFSItemCollection itemCollection)
        {
            ItemPath = itemPath;
            ItemsLoaderState = itemsLoaderState;
            ItemCollection = itemCollection;
            BeginLoadItems();
        }

        public event EventHandler UpdateLoaderState;

        public string ItemPath { get; private set; }

        public bool IsLoadingItems { get; private set; }

        public bool IsLoadedItems => ItemsLoaderState is FSItemsLoadedState;

        public FSItemLoaderState ItemsLoaderState
        {
            get => _itemsLoaderState;
            set
            {
                _itemsLoaderState = value;
                OnUpdateLoaderState();
            }
        }

        public IFSItemCollection ItemCollection { get; set; }

        public void BeginLoadItems()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            if (IsLoadingItems) return;
            Task.Factory.StartNew(() =>
            {
                IsLoadingItems = true;
                ItemsLoaderState.LoadItems(this);
                IsLoadingItems = false;
            }, _cancellationTokenSource.Token);
        }
        private CancellationTokenSource _cancellationTokenSource;
        public void EndLoadItems()
        {
            _cancellationTokenSource.Cancel();
            IsLoadingItems = false;
        }

        public void ValidateItemState()
        {
            if (!IsLoadingItems)
                ItemsLoaderState.ValidateItemState(this);
        }

        //private void CancelLoading()

        private void OnUpdateLoaderState() => UpdateLoaderState?.Invoke(this, EventArgs.Empty);
    }
}
