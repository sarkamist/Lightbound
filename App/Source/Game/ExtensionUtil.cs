using System;

namespace GameJam
{
    public static class ExtensionUtil
    {
        public static float NextFloat(this Random random, float minValue, float maxValue)
        {
            float randomFloat = (float) random.NextDouble() * (maxValue - minValue) + minValue;

            return randomFloat;
        }
    }
}
