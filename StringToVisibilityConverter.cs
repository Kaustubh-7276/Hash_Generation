using System;
using System.Windows;
using System.Windows.Data;

namespace HashGenerator.VIewModel
{
    // Fixes "StringToVisibilityConverter does not exist"
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }

    // Fixes "InverseBooleanToVisibilityConverter does not exist"
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool val = value is bool b && b;
            return val ? Visibility.Collapsed : Visibility.Visible;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) => throw new NotImplementedException();
    }

    // NEW: Required for the IsEnabled binding in the Bulk Template
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(value is bool b && b);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }

    // NEW: Required for the Result Header visibility in the Bulk Template
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value is int i && i > 0) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}
