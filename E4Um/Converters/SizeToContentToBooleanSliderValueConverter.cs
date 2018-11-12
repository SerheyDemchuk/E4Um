using System;
using System.Windows.Data;
using System.Windows.Media;

namespace E4Um.Converters
{
    class SizeToContentToBooleanSliderValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            if (value is string)
            {
                string widthToContent = (string)value;
                if (widthToContent == "Width")
                    return false;
                else if (widthToContent == "Manual")
                    return true;
            }
            // You can support here more source types if you wish
            // For the example I throw an exception

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
