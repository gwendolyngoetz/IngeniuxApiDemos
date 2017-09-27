namespace IngeniuxApiDemos.Common.Extensions
{
    public static class StringExtensions
    {        
        /// <summary>
        /// Wraps string.IsNullOrWhiteSpace(...) to make life simpler, much much simpler.
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// Convert a int value string to an int
        /// </summary>
        /// <returns>The int value of its string representation or 0 if failed.</returns>
        public static int ToInt(this string source)
        {
            if (source.IsNullOrWhiteSpace())
            {
                return 0;
            }

            return int.TryParse(source, out int result) ? result : 0;
        }
    }
}
