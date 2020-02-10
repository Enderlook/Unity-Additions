using System;

namespace Enderlook.Extensions
{
    public static class RandomExtension
    {
        public static int Range(this Random source, int min, int max) => source.Next(min, max);

        public static int Range(this Random source, int max) => source.Next(max);

        public static double Range(this Random source, double min, double max) => source.NextDouble() * (max - min) + min;

        public static double Range(this Random source, double max) => source.NextDouble() * max;

        public static float Range(this Random source, float min, float max) => (float)source.Range(min, max);

        public static float Range(this Random source, float max) => (float)source.Range(max);
    }
}