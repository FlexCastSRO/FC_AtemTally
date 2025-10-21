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
                        return (SolidColorBrush)new BrushConverter().ConvertFrom("#ff0000");
                    }
                    else
                    {
                        return (SolidColorBrush)new BrushConverter().ConvertFrom("#222222");
                    }
                }
                else if (stringParameter == "PRW")
                {
                    if (booleanValue)
                    {
                        return (SolidColorBrush)new BrushConverter().ConvertFrom("#00ff00");
                    }
                    else
                    {
                        return (SolidColorBrush)new BrushConverter().ConvertFrom("#222222");
                    }
                }
            }
            return (SolidColorBrush)new BrushConverter().ConvertFrom("#000000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
