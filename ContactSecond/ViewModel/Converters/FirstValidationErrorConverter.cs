using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ContactSecond.ViewModel.Converters
{
    public class FirstValidationErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Проверяем, что value - это коллекция ValidationError
            var errors = value as ReadOnlyCollection<ValidationError>;
            if (errors != null && errors.Count > 0)
            {
                // Возвращаем текст первой ошибки
                return errors[0].ErrorContent;
            }
            // Если ошибок нет, возвращаем null
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

