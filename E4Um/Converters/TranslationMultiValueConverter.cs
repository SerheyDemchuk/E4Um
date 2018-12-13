using System;
using System.Windows.Data;
using System.Collections.Generic;

namespace E4Um.Converters
{
    class TranslationMultiValueConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == values)
            {
                return null;
            }

            if (values is object[])
            {
                List<object> translation = new List<object>(values);
                return translation; 
            }

            Type type = values.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
