using System;

namespace Android.Message
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// unix时间戳
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToTimestamp(this DateTime value)
        {
            return (int)((value.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }
    }
}
