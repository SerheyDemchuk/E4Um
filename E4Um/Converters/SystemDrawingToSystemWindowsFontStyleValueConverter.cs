using System;
using System.Windows.Data;
using System.Drawing;

namespace E4Um.Converters
{
    class SystemDrawingToSystemWindowsFontStyleValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            if (value is FontStyle)
            {
                FontStyle fontStyle = (FontStyle)value;
                if (fontStyle.ToString().Contains("Italic"))
                    return "Italic";
                else return "Normal";
            }

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            throw new NotImplementedException();
        }
    }
}
