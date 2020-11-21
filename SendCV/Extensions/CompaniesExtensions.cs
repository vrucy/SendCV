using SendCV.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace SendCV.Extensions
{
    public class CompaniesExtensions : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CompanyCredentials credentials = value as CompanyCredentials;
            return credentials.DateEmailSend.ToString("dd/MM/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
