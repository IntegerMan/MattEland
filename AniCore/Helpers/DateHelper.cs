using System;

namespace Ani.Core.Helpers
{
    /// <summary>
    /// Utility logic dealing with dates.
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Takes a date and creates a new UTC Date component out of the date the original date is representing.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A new UTC date representing midnight on the specified date.</returns>
        public static DateTime ToUtcDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}