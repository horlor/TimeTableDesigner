using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace TimetableDesigner.Graphics.Converters
{
    class ErrorToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; } = new SolidColorBrush(Windows.UI.Colors.White);
        public Brush FalseBrush { get; set; } = new SolidColorBrush(Windows.UI.Colors.LightCoral);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            System.Diagnostics.Debug.WriteLine("brusH: " + (bool)value);
            if ((bool)value)
                return TrueBrush;
            else
                return FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (ReferenceEquals(TrueBrush, value))
                return true;
            return false;
        }
    }
}
