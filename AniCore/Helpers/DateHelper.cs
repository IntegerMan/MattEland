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

        /// <summary>
        /// Determines whether the two dates are on the same day. 
        /// </summary>
        /// <remarks>
        /// This is a convenience / safety method for LINQ which can hiccup with date comparisons in lambdas.
        /// </remarks>
        /// <param name="date1">The first date.</param>
        /// <param name="date2">The second date.</param>
        /// <returns><c>true</c> if date1 and date2 occur on the same date, <c>false</c> otherwise.</returns>
        public static bool AreSameDate(DateTime date1, DateTime date2)
        {
            return date1.Date == date2.Date;
        }
    }
}