namespace EasyCaching.Core.Internal
{
    using System;
    using System.Threading;

    public static class RandomHelper
    {
#if NET6_0_OR_GREATER
        private static Random Instance => Random.Shared;
#else
        private static Random Instance => _local.Value;

        private static readonly Random _global = new Random();
        private static readonly ThreadLocal<Random> _local = new ThreadLocal<Random>(() =>
        {
            int seed;
            lock (_global)
            {
                seed = _global.Next();
            }
            return new Random(seed);
        });
#endif

        public static int GetNext(int min, int max)
        {
            return Instance.Next(min, max);
        }
    }
}
