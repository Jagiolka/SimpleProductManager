using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleProductManager.Gui.Converter;

public class PriceFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal decimalValue)
        {
            return decimalValue.ToString("F2", culture);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            if (decimal.TryParse(stringValue, NumberStyles.Any, culture, out decimal result))
            {
                return Math.Round(result, 2); // 0.00
            }
        }
        return 0m;
    }
}