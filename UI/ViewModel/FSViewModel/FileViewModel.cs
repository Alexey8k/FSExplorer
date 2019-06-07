using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.Model;
using UI.ViewModel.FSViewModel.Common;

namespace UI.ViewModel.FSViewModel
{
    internal class FileViewModel : FSItemViewModel
    {
        public FileViewModel(FSItemModel itemModel) : base(itemModel)
        {
        }
    }
}
