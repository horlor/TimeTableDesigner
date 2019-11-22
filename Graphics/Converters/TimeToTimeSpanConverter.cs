using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;
using Windows.UI.Xaml.Data;

namespace TimetableDesigner.Graphics.Converters
{
    public class TimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                Time t = (Time)value;
                System.Diagnostics.Debug.WriteLine(t.Hour + ":" + t.Minute);
                return new TimeSpan(t.Hour, t.Minute, 0);
            }
            return new TimeSpan();
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                TimeSpan timeSpan = (TimeSpan)value;
                return new Time(timeSpan.Hours, timeSpan.Minutes);
            }
            return new Time(0, 0);
        }
    }
}
