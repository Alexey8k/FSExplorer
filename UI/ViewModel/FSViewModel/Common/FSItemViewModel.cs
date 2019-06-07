using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UI.ViewModel.Common;
using UI.Extension;
using BLL.Abstract;
using BLL.Model;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using System.ComponentModel;
using System.Windows;
using BLL;
using System.Runtime.InteropServices;

namespace UI.ViewModel.FSViewModel.Common
{
    public abstract class FSItemViewModel : BaseViewModel, IFSItemViewModel, IComparable<FSItemViewModel>
    {
        #region Data
        protected FSItemModel _itemModel;

        #endregion // Data

        #region Constructor

        public FSItemViewModel(FSItemModel itemModel)
        {
            _itemModel = itemModel;
        }

        #endregion //Constructor

        #region Presentation
        public string DisplayName => _itemModel.DisplayName;

        public string TypeName => _itemModel.TypeName;

        public string Path => _itemModel.Path;

        public BitmapSource SmallIcon => _itemModel.SmallIcon;
                                         
        public BitmapSource LargeIcon => _itemModel.LargeIcon;

        public ICommand OpenCommand => new ActionCommand(() => 
        {
            try
            {
                if (_itemModel.IsNode)
                    System.Diagnostics.Process.Start("explorer.exe", Path);
                else
                    System.Diagnostics.Process.Start(Path);
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        });

        #endregion // Presentation

        #region IComparable

        public int CompareTo(FSItemViewModel other) => _itemModel.CompareTo(other._itemModel);

        #endregion // IComparable
    }
}
