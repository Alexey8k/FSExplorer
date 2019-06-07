using BLL.Abstract;
using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Abstract;
using UI.ViewModel.FSViewModel;
using UI.ViewModel.FSViewModel.Common;

namespace UI.Concrete
{
    public class FSItemViewModelFactory : IFSItemViewModelFactory
    {
        private IFSItemCollectionFactory _itemCollectionFactory;
        private IFSItemManagerFactory _itemManagerFactory;

        public FSItemViewModelFactory(
            IFSItemCollectionFactory itemCollectionFactory,
            IFSItemManagerFactory itemManagerFactory)
        {
            _itemCollectionFactory = itemCollectionFactory;
            _itemManagerFactory = itemManagerFactory;
        }

        public IFSItemViewModel CreateDirectoryViewModel(FSItemModel itemModel)
            => new DirectoryViewModel(itemModel, _itemCollectionFactory, _itemManagerFactory);

        public IFSItemViewModel CreateDriveViewModel(FSItemModel itemModel)
            => new DriveViewModel(itemModel, _itemCollectionFactory, _itemManagerFactory);

        public IFSItemViewModel CreateDummyViewModel() => dummyViewModel;

        public IFSItemViewModel CreateFileViewModel(FSItemModel itemModel)
            => new FileViewModel(itemModel);

        private static readonly IFSItemViewModel dummyViewModel = new FSDummyViewModel();
    }
}
