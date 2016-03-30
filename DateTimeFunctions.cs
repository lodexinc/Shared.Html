using System;
using System.Globalization;

namespace Shared.Html
{
    public static class DateTimeFunctions
    {
        /// <summary>
        /// Returns the Week Number for a given Datetime, as per ISO-8601.
        /// </summary>
        /// <param name="dayInWeek"></param>
        /// <returns></returns>
        public static int GetWeekNumber(DateTime dayInWeek)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dayInWeek, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        /// <summary>
        /// Provides the first date of a given week (Monday), for a given year.  This implementation mimics ISO-8601, relying on the first 4-day week containing a Thursday.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        public static DateTime FirstMonday(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            return firstThursday.AddDays(weekNum * 7).AddDays(-3);
        }

        /// <summary>
        /// Provides the first date of a given week (Monday), for a given year.  This implementation mimics ISO-8601, relying on the first 4-day week containing a Thursday.
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static DateTime FirstMonday(this DateTime theDate)
        {
            return FirstMonday(theDate.Year, GetWeekNumber(theDate));
        }

        /// <summary>
        /// Returns the ending date of a given week (Sunday), for a given year.  This implementation mimics ISO-8601, relying on the first 4-day week containing a Thursday.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        public static DateTime EndingSunday(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            return firstThursday.AddDays(weekNum * 7).AddDays(3);
        }

        /// <summary>
        /// Returns the ending date of a given week (Sunday), for a given year.  This implementation mimics ISO-8601, relying on the first 4-day week containing a Thursday.
        /// </summary>
        /// <param name="theDate"></param>
        /// <returns></returns>
        public static DateTime EndingSunday(this DateTime theDate)
        {
            return EndingSunday(theDate.Year, GetWeekNumber(theDate));
        }

        /// <summary>
        /// Returns the suffix for the day of the month, given a date.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>string</returns>
        public static string DaySuffix(this DateTime dt)
        {
            switch (dt.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        /// <summary>
        /// Return the number relating to the quarter of the provided date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>int</returns>
        public static int GetQuarter(DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return 1;
            else if (date.Month >= 7 && date.Month <= 9)
                return 2;
            else if (date.Month >= 10 && date.Month <= 12)
                return 3;
            else
                return 4;

        }
    }
}