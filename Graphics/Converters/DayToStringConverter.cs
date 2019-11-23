using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;
using Windows.UI.Xaml.Data;

namespace TimetableDesigner.Graphics.Converters
{
    public class DayToStringConverter :IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                switch ((Day)value)
                {
                    case Day.Monday: return "Monday";
                    case Day.Tuesday: return "Tuesday";
                    case Day.Wednesday: return "Wednesday";
                    case Day.Thursday: return "Thursday";
                    case Day.Friday: return "Friday";
                    case Day.Saturday: return "Saturday";
                    case Day.Sunday: return "Sunday";
                }
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                switch (value as String)
                {
                    case "Monday": return Day.Monday;
                    case "Tuesday": return Day.Tuesday;
                    case "Wednesday": return Day.Wednesday;
                    case "Thursday": return Day.Thursday;
                    case "Friday": return Day.Friday;
                    case "Saturday": return Day.Saturday;
                    case "Sunday": return Day.Sunday;
                }
            }
            return Day.Monday;
        }
    }
}
