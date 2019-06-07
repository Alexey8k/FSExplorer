using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BLL.Abstract;
using BLL.Model;
using UI.ViewModel.FSViewModel.Common;

namespace UI.ViewModel.FSViewModel
{
    internal class FSDummyViewModel : IFSItemViewModel
    {
        public string Path => string.Empty;

        public string DisplayName => string.Empty;

        public BitmapSource SmallIcon => null;

        public BitmapSource LargeIcon => null;

        public bool IsSelected { get; set; }

        public bool IsExpanded { get; set; }
    }
}
