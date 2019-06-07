using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Abstract;
using UI.Enum;
using UI.ViewModel.Common;
using UI.ViewModel.FSViewModel;
using UI.ViewModel.FSViewModel.Common;

namespace UI.ViewModel
{
    internal class FSExplorerViewModel : BaseViewModel
    {
        private IFSItemViewModelFactory _itemViewModelFactory;
        private IFSExplorer _explorer;
        private SizeUnits _sizeUnits = SizeUnits.Byte;

        public FSExplorerViewModel(IFSItemViewModelFactory itemViewModelFactory, IFSExplorer explorer)
        {
            _itemViewModelFactory = itemViewModelFactory;
            _explorer = explorer;

            Drives = new ObservableCollection<IFSItemViewModel>(_explorer
                .GetDrives(BLL.DriveTypes.Fixed)
                .Select(d => _itemViewModelFactory.CreateDriveViewModel(d)));
        }

        public ObservableCollection<IFSItemViewModel> Drives { get; }

        public SizeUnits[] AllSizeUnits { get; } = System.Enum.GetValues(typeof(SizeUnits)).Cast<SizeUnits>().ToArray();

        public SizeUnits SizeUnits
        {
            get => _sizeUnits;
            set
            {
                _sizeUnits = value;
                OnPropertyChanged();
            }
        }
    }
}
