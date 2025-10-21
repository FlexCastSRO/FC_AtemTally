using System.Windows.Data;
using System.Windows.Media;
using System.Globalization;


namespace FC_AtemTallyServer.Converters
{
    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    class BoolToTallyColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var booleanValue = (bool)value;
            string stringParameter = (string)parameter;

            if (stringParameter != null)
            {
                if (stringParameter == "PGM")
                {
                    if (booleanValue)
                    {
                        return GetSolidColorBrushFromHex("#ff0000");
                    }
                    else
                    {
                        return GetSolidColorBrushFromHex("#222222");
                    }
                }
                else if (stringParameter == "PRW")
                {
                    if (booleanValue)
                    {
                        return GetSolidColorBrushFromHex("#00ff00");
                    }
                    else
                    {
                        return GetSolidColorBrushFromHex("#222222");
                    }
                }
            }
            return GetSolidColorBrushFromHex("#000000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private SolidColorBrush GetSolidColorBrushFromHex(string hex)
        {
            BrushConverter brushConverter = new BrushConverter();
            object? brushConverterObject = brushConverter.ConvertFromString(hex);

            if (brushConverterObject != null)
            {
                return (SolidColorBrush)brushConverterObject;
            }

            return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
    }
}
