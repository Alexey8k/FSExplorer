using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Abstract;

namespace UI.ViewModel.FSViewModel.Common
{
    public interface IFSNodeViewModel : IFSItemViewModel
    {
        ObservableCollection<IFSItemViewModel> Items { get; }
        bool IsExpanded { get; set; }
        bool IsLoadedItems { get; }
    }
}
