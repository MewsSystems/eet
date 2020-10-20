using System;

namespace Mews.Eet
{
    public class DateTimeWithTimeZone
    {
        public static TimeZoneInfo CzechTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");

        public DateTimeWithTimeZone(DateTime dateTime, TimeZoneInfo timezoneInfo)
        {
            DateTime = dateTime;
            TimeZoneInfo = timezoneInfo;
        }

        public DateTime DateTime { get; }

        public TimeZoneInfo TimeZoneInfo { get; }
    }
}