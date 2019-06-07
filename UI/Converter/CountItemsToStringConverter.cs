using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UI.Converter
{
    internal class CountItemsToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
            => value[2] is UnauthorizedAccessException exception 
            ? exception.Message 
            : $"Файлов: {value[0]}; Папок: {value[1]}";
            

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException("Cannot convert back");
    }
}
