
using System;
namespace FinishLine.Library.Infrastructure
{
    public static class Extensions
    {
        public static bool Empty(this string data)
        {
            return string.IsNullOrEmpty(data);
        }

        public static bool Empty(this DateTime date)
        {
            return date == DateTime.MinValue;
        }
    }
}
