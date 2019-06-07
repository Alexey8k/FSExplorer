using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModel.FSViewModel.Common;

namespace UI.Abstract
{
    public interface IFSItemViewModelFactory
    {
        IFSItemViewModel CreateDriveViewModel(FSItemModel itemModel);
        IFSItemViewModel CreateDirectoryViewModel(FSItemModel itemModel);
        IFSItemViewModel CreateFileViewModel(FSItemModel itemModel);
        IFSItemViewModel CreateDummyViewModel();
    }
}
