namespace Cardinal.AspNetCore.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsBetween(this int value, int minValue, int MaxValue, bool includeLimits = true)
        {
            if (includeLimits)
            {
                return value >= minValue && value <= MaxValue;
            }
            else
            {
                return value > minValue && value < MaxValue;
            }
        }
    }
}
