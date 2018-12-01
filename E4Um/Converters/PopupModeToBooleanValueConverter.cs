using System;
using System.Windows.Data;

namespace E4Um.Converters
{
    class PopupModeToBooleanValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            if (value is string)
            {
                string mode = (string)value;
                if (mode == "popup")
                    return true;
                else return false;
            }

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            if (value is bool)
            {
                return "popup";
            }

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }
    }
}
