using System;

namespace MFL.Common.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTimeFromUnix(this string str)
        {
            DateTime date = DateTime.UnixEpoch;

            if (long.TryParse(str, out long unixTimestamp))
            {
                var offset = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
                date = offset.UtcDateTime.ToLocalTime();
            }

            return date;
        }

        public static int ToInt(this string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }

            return 0;
        }
    }
}
