namespace SmartTalent.Hotel.Api.Utilities
{
    public static class Util
    {
        /// <summary>
        /// Convert a date to its natural form without time
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ConvertDateToShort(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
