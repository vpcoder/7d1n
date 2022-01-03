using System;

namespace com.baensi.sdon
{

    public static class DateTimeAdditions
    {

        #region Hidden Fields

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #endregion

        /// <summary>
        /// Преобразует время в UNIX-TIME формат
        /// </summary>
        /// <param name="time">Время, которое надо преобразовать</param>
        /// <returns>Возвращает количество миллисекунд с 1 января 1970 года</returns>
        public static long ToTimestamp(this DateTime time)
        {
            return (long)(time-UnixEpoch).TotalMilliseconds;
        }

        /// <summary>
        /// Преобразует UNIX-TIME дату в c# дату
        /// </summary>
        /// <param name="timestamp">Количество миллисекунд прошедших с 1 января 1970 года</param>
        /// <returns>Возвращает c# дату</returns>
        public static DateTime ToTime(this long timestamp)
        {
            return UnixEpoch.AddMilliseconds(timestamp);
        }

    }

}
