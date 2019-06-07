using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using BLL.Abstract;
using BLL.Model;
using Microsoft.Expression.Interactivity.Core;
using UI.Abstract;
using UI.Extension;

namespace UI.ViewModel.FSViewModel.Common
{
    public abstract class FSNodeViewModel : FSItemViewModel, IFSNodeViewModel, IFSItemManaged
    {
        protected bool _isExpanded = false;
        protected IFSItemManager _itemManager;
        protected IFSItemCollection _itemCollection;

        public FSNodeViewModel(
            FSItemModel itemModel,
            IFSItemCollectionFactory itemCollectionFactory,
            IFSItemManagerFactory itemManagerFactory) : base(itemModel)
        {
            _itemCollection = itemCollectionFactory.Create(Items);
            _itemManager = itemManagerFactory.Create(Path, _itemCollection);
            _itemManager.UpdateLoaderState += OnUpdateLoaderState;
        }

        public IFSItemManager ItemManager => _itemManager;

        #region Presentation

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                OnPropertyChanged();

                if (_isExpanded)
                {
                    _cancellationTokenSourcesExpanded.ForEach(ctse => ctse.Cancel());
                    _cancellationTokenSourcesExpanded.Clear();
                    _itemManager.BeginManagement();
                }
                else
                {
                    _cancellationTokenSourcesCollapse.ForEach(ctse => ctse.Cancel());
                    _cancellationTokenSourcesCollapse.Clear();
                    _itemManager.EndManagement();
                    Task.Factory.StartNew(() => СollapseChildItems());
                }
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            set
            {
                if (!(_isSelected = value))
                    return;
                CountItems();
            }
        }

        public ObservableCollection<IFSItemViewModel> Items { get; } = new ObservableCollection<IFSItemViewModel>();

        public int CountFiles { get; private set; }

        public int CountFolders { get; private set; }

        public UnauthorizedAccessException UnauthorizedAccessException { get; private set; }

        public bool IsLoadedItems { get; private set; }

        public ICommand ExpandedAllChildFolderCommand => new ActionCommand(() =>
        {
            if (!IsExpanded)
                IsExpanded = true;
            Task.Factory.StartNew(() => ExpandChildItems());

        });

        #endregion // Presentation


        public void ValidateItemState()
            => _itemManager.ValidateItemState();

        private List<CancellationTokenSource> _cancellationTokenSourcesExpanded = new List<CancellationTokenSource>();
        private List<CancellationTokenSource> _cancellationTokenSourcesCollapse = new List<CancellationTokenSource>();
        private void СollapseChildItems()
        {
            var cancellationTokenSources = new CancellationTokenSource();
            _cancellationTokenSourcesExpanded.Add(cancellationTokenSources);
            _itemCollection.ThreadSafeAction((items) =>
            {
                Parallel.ForEach(
                    Items.Where(item => item is IFSNodeViewModel).Cast<IFSNodeViewModel>(),
                    new ParallelOptions { CancellationToken = cancellationTokenSources.Token },
                    (item) =>
                    {
                        if (!item.IsExpanded) return;
                        item.IsExpanded = false;
                    });
            });
        }

        private void ExpandChildItems()
        {
            var cancellationTokenSources = new CancellationTokenSource();
            _cancellationTokenSourcesCollapse.Add(cancellationTokenSources);
            _itemCollection.ThreadSafeAction((items) =>
            {
                Parallel.ForEach(
                    Items.Where(item => item is IFSNodeViewModel).Cast<IFSNodeViewModel>(),
                    new ParallelOptions { CancellationToken = cancellationTokenSources.Token },
                    (item) =>
                    {
                        if (item.IsExpanded) return;
                        item.IsExpanded = true;
                    });
            });
        }

        private void CountItems()
        {
            try
            {
                Task.Factory.StartNew((enumerate) =>
                {
                    CountFiles ^= CountFiles;
                    foreach (var item in (IEnumerable<string>)enumerate)
                    {
                        if (!_isSelected) return;
                        ++CountFiles;
                        OnPropertyChanged(nameof(CountFiles));
                    }

                }, Directory.EnumerateFiles(Path));

                Task.Factory.StartNew((enumerate) =>
                {
                    CountFolders ^= CountFolders;
                    foreach (var item in (IEnumerable<string>)enumerate)
                    {
                        if (!_isSelected) return;
                        ++CountFolders;
                        OnPropertyChanged(nameof(CountFolders));
                    }
                }, Directory.EnumerateDirectories(Path));
            }
            catch (UnauthorizedAccessException ex)
            {
                UnauthorizedAccessException = ex;
            }
        }

        private void OnUpdateLoaderState(object sender, EventArgs args)
        {
            IsLoadedItems = ((IFSItemLoader)sender).IsLoadedItems;
            OnPropertyChanged(nameof(IsLoadedItems));
        }
    }
}
