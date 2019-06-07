using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UI.ViewModel.FSViewModel.Common
{
    public interface IFSItemViewModel : IFSItem
    {
        string DisplayName { get; }
        string Path { get; }
        BitmapSource SmallIcon { get; }
        BitmapSource LargeIcon { get; }
    }
}
