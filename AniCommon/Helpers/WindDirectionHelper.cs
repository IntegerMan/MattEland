namespace AniCommon.Helpers
{
    /// <summary>
    /// Contains utility methods for dealing with wind
    /// </summary>
    public static class WindDirectionHelper
    {
        /// <summary>
        /// Gets the cardinal direction (e.g. NNE, SW) from a degree value.
        /// </summary>
        /// <param name="degree">The degree.</param>
        /// <returns>A cardinal direction string.</returns>
        public static string GetCardinalDirection(double degree)
        {
            if (degree <= 11.25)
                return "N";
            if (degree <= 33.75)
                return "NNE";
            if (degree <= 56.25)
                return "NE";
            if (degree <= 78.75)
                return "ENE";
            if (degree <= 101.25)
                return "E";
            if (degree <= 123.75)
                return "ESE";
            if (degree <= 146.25)
                return "SE";
            if (degree <= 168.75)
                return "SSE";
            if (degree <= 191.25)
                return "S";
            if (degree <= 213.75)
                return "SSW";
            if (degree <= 236.25)
                return "SW";
            if (degree <= 258.75)
                return "WSW";
            if (degree <= 281.25)
                return "W";
            if (degree <= 303.75)
                return "WNW";
            if (degree <= 326.25)
                return "NW";
            if (degree <= 348.75)
                return "NNW";

            return "N";
        }
    }
}