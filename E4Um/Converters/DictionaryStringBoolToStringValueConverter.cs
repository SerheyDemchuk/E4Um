using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace E4Um.Converters
{
    class DictionaryStringBoolToStringValueConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }

            if (value is Dictionary<string, bool>)
            {
                Dictionary<string, bool> dictionary = (Dictionary<string, bool>)value;
                string translation = "";
                foreach (KeyValuePair<string, bool> entry in dictionary)
                {
                    translation = entry.Key;
                }
                translation = translation.Remove(0,1);
                return translation;
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
