using UnityEngine;

namespace LordBreakerX.Utilities
{
    public static class PercentageUtility
    {
        public const float MIN_PERCENTAGE_VALUE = 0f;
        public const float MAX_PERCENTAGE_VALUE = 100f;

        public const float MIN_NORMALIZED_PERCENTAGE_VALUE = 0f;
        public const float MAX_NORMALIZED_PERCENTAGE_VALUE = 1f;

        public static float PercentageNormalized(float value, float minValue = MIN_PERCENTAGE_VALUE, float maxValue = MAX_PERCENTAGE_VALUE)
        {
            float clampedValue = Mathf.Clamp(value, minValue, maxValue);
            float percentage = (clampedValue - minValue) / (maxValue - minValue);
            return Mathf.Clamp(percentage, MIN_NORMALIZED_PERCENTAGE_VALUE, MAX_NORMALIZED_PERCENTAGE_VALUE);
        }

        public static float Percentage(float value, float minValue = MIN_PERCENTAGE_VALUE, float maxValue = MAX_PERCENTAGE_VALUE)
        {
            return PercentageNormalized(value, minValue, maxValue) * MAX_PERCENTAGE_VALUE;
        }

        public static float InvertedPercentageNormalized(float value, float minValue = MIN_PERCENTAGE_VALUE, float maxValue = MAX_PERCENTAGE_VALUE)
        {
            return MAX_NORMALIZED_PERCENTAGE_VALUE - PercentageNormalized(value, minValue, maxValue);
        }

        public static float InvertedPercentage(float value, float minValue = MIN_PERCENTAGE_VALUE, float maxValue = MAX_PERCENTAGE_VALUE)
        {
            return MAX_PERCENTAGE_VALUE - Percentage(value, minValue, maxValue);
        }

        public static float MapNormalizedPercentage(float percentage, float minValue, float maxValue)
        {
            float clampedPercentage = Mathf.Clamp(percentage, MIN_NORMALIZED_PERCENTAGE_VALUE, MAX_NORMALIZED_PERCENTAGE_VALUE);
            return minValue + (maxValue - minValue) * clampedPercentage;
        }

        public static float MapPercentage(float percentage, float minValue, float maxValue)
        {
            float clampedPercentage = Mathf.Clamp(percentage, MIN_PERCENTAGE_VALUE, MAX_PERCENTAGE_VALUE);
            return MapNormalizedPercentage(clampedPercentage / 100f, minValue, maxValue);
        }
    }
}
