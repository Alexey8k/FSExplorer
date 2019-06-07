using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.ViewModel.FSViewModel.Common;

namespace UI.DataTemplateSelector
{
    class FSItemInfoDataTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public DataTemplate FSFileInfoDataTemplate { get; set; }
        public DataTemplate FSImageFileInfoDataTemplate { get; set; }
        public DataTemplate FSDirectoryInfoDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is FSNodeViewModel FSNode)
                return FSDirectoryInfoDataTemplate;
            else if (item is FSItemViewModel FSItem)
                return IsImageFile(FSItem.Path)
                    ? FSImageFileInfoDataTemplate
                    : FSFileInfoDataTemplate;
            return null;
        }

        private bool IsImageFile(string path) => new[] { ".png", ".jpeg", ".jpg", ".bmp" }.Any(ex => ex == Path.GetExtension(path));
    }
}
