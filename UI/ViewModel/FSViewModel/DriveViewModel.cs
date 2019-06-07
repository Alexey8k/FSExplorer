using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Model;
using UI.Abstract;
using UI.ViewModel.FSViewModel.Common;

namespace UI.ViewModel.FSViewModel
{
    internal class DriveViewModel : FSNodeViewModel
    {
        public DriveViewModel(FSItemModel itemModel, IFSItemCollectionFactory itemCollectionrFactory, IFSItemManagerFactory itemManagerFactory)
            : base(itemModel, itemCollectionrFactory, itemManagerFactory)
        {
            //_itemManager.LoadItems();
        }
    }
}
