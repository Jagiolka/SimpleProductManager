namespace SimpleProductManager.Gui.Converter;

using SimpleProductManager.DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

public class CategoryListToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is List<ProductCategoryModel> categoryList)
        {
            return string.Join(", ", categoryList.Select(category => category.Name));
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
