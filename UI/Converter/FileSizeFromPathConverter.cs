using BLL;
using FSIcon;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UI.Enum;
using UI.Extension;

namespace UI.Converter
{
    internal class FileSizeFromPathConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
            => value[0] is string path && value[1] is SizeUnits sizeUnits
            ? new FileInfo(path as string).Length.ToSizeUnits(sizeUnits).Round(countNumderAfterZero: 2).ToString()
            : "";

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException("Cannot convert back");
    }
}
