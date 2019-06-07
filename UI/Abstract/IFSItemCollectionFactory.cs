using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModel.FSViewModel.Common;

namespace UI.Abstract
{
    public interface IFSItemCollectionFactory
    {
        IFSItemCollection Create(ObservableCollection<IFSItemViewModel> items);
    }
}
