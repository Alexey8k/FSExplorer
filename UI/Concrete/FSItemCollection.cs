using BLL.Abstract;
using BLL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using UI.Abstract;
using UI.ViewModel.FSViewModel.Common;

namespace UI.Concrete
{
    class FSItemCollection : IFSItemCollection
    {
        private object _itemsLock = new object();
        private IFSItemViewModelFactory _itemViewModelFactory;
        private ObservableCollection<IFSItemViewModel> _items;
        private ICollectionView _itemCollectionView;

        public FSItemCollection(ObservableCollection<IFSItemViewModel> items, IFSItemViewModelFactory itemViewModelFactory)
        {
            _items = items;
            _itemViewModelFactory = itemViewModelFactory;

            EnableCollectionSynchronization();
        }

        public void Add(FSItemModel itemModel)
        {
            lock (_itemsLock)
                if (!_items.AsParallel().Any(item => item.Path == itemModel.Path))
                    _items.Add(CreateItemViewModel(itemModel));
        }

        public bool Remove(string path)
        {
            lock (_itemsLock)
                return _items.Remove(_items.FirstOrDefault(vm => vm.Path == path));
        }

        public int Count
        {
            get
            {
                lock (_itemsLock)
                    return _items.Count;
            }
        }

        public IFSItemManager this[string itemPath]
        {
            get
            {
                lock (_itemsLock)
                    return (_items.FirstOrDefault(item => item.Path == itemPath) is IFSItemManaged itemManaged) ? itemManaged.ItemManager : null;
            }
        }

        public IEnumerable<IFSItemManager> ItemManagers
            => _items.Where(item => item is IFSItemManaged).Cast<IFSItemManaged>().Select(item => item.ItemManager);

        public void Clear()
        {
            lock (_itemsLock)
                _items.Clear();
        }

        public void ThreadSafeAction(Action<IEnumerable<IFSItem>> action)
        {
            lock (_itemsLock)
                action?.Invoke(_items);
        }

        private void EnableCollectionSynchronization()
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                BindingOperations.EnableCollectionSynchronization(_items, _itemsLock);
                _itemCollectionView = CollectionViewSource.GetDefaultView(_items);
                ((ListCollectionView)_itemCollectionView).CustomSort = Comparer<FSItemViewModel>.Default;
            }));
        }

        private IFSItemViewModel CreateItemViewModel(FSItemModel itemModel)
            => itemModel == null
            ? _itemViewModelFactory.CreateDummyViewModel()
            : itemModel.IsNode
            ? _itemViewModelFactory.CreateDirectoryViewModel(itemModel)
            : _itemViewModelFactory.CreateFileViewModel(itemModel);
    }
}
