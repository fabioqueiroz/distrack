using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DisTrackProject.Helper
{
    public class DateManager
    {
        public static int GetWeekOfTheYear(DateTime date)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            int weekNum = culture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return weekNum;
        }
    }
}
