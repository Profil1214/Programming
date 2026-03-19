using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ContactSecond.ViewModel.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует исходное булевое значение в противоположное.
        /// </summary>
        /// <param name="value">Исходное значение для преобразования.</param>
        /// <param name="targetType">Целевой тип преобразования.</param>
        /// <param name="parameter">Параметр конвертера.</param>
        /// <param name="culture">Информация о культуре.</param>
        /// <returns>Противоположное булевое значение.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        /// <summary>
        /// Выполняет обратное преобразование (также инвертирует значение).
        /// </summary>
        /// <param name="value">Значение для обратного преобразования.</param>
        /// <param name="targetType">Целевой тип преобразования.</param>
        /// <param name="parameter">Параметр конвертера.</param>
        /// <param name="culture">Информация о культуре.</param>
        /// <returns>Противоположное булевое значение.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
