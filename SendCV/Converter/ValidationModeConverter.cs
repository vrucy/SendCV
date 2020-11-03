using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace SendCV.Converter
{
    class ValidationModeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? index = value as int?;
            if (index == 0)
                return GridValidationMode.None;
            else if (index == 1)
                return GridValidationMode.InEdit;
            else
                return GridValidationMode.InView;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
