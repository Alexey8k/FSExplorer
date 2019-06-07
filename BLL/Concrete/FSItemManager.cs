using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class FSItemManager : IFSItemManager
    {
        private IFSItemLoader _itemLoader;
        private IFSItemWatcher _watcher;

        public FSItemManager(IFSItemLoader itemLoader, IFSItemWatcher watcher)
        {
            _itemLoader = itemLoader;
            _watcher = watcher;
        }

        public event EventHandler UpdateLoaderState
        {
            add => _itemLoader.UpdateLoaderState += value;
            remove => _itemLoader.UpdateLoaderState -= value;
        }

        public IFSItemCollection ItemCollection { get; set; }

        public bool IsManagement { get; private set; }

        public void BeginManagement()
        {
            if (IsManagement || IsEmptyItem) return;

            IsManagement = true;

            _itemLoader.BeginLoadItems();
            _watcher.BeginWatch();
        }

        public void EndManagement()
        {
            if (!IsManagement) return;

            IsManagement = false;

            _itemLoader.EndLoadItems();
            _watcher.EndWatch();
        }

        public void ValidateItemState()
        {
            if (!IsManagement)
                _itemLoader.ValidateItemState();
        }

        private bool IsEmptyItem => _itemLoader.ItemCollection.Count == 0 && _itemLoader.IsLoadedItems;
    }
}
