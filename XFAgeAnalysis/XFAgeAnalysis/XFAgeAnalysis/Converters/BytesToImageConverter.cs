using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace XFAgeAnalysis.Converters
{
    public class BytesToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageBytes)
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
