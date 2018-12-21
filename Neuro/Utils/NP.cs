using System;

namespace Neuro.Utils
{
    /// <summary>
    /// reference:
    /// https://github.com/andrewkirillov/AForge.NET/blob/master/Sources/Core/ThreadSafeRandom.cs
    /// </summary>
    public sealed class ThreadSafeRandom : Random
    {
        private readonly object sync = new object();

        /// <summary>
        /// Initializes a new instance of the ThreadSafeRandom
        /// </summary>
        public ThreadSafeRandom() : base() { }

        /// <summary>
        /// A number used to calculate a starting value for the pseudo-random number sequence.
        /// If a negative number is specified, the absolute value of the number is used.
        /// </summary>
        /// <param name="seed"></param>
        public ThreadSafeRandom(int seed) : base(seed) { }

        /// <summary>
        /// Returns a nonnegative random number.
        /// </summary>
        /// <returns></returns>
        public override int Next()
        {
            lock (sync) return base.Next();
        }

        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public override int Next(int maxValue)
        {
            lock (sync) return base.Next(maxValue);
        }

        /// <summary>
        /// Returns a random number within a specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public override int Next(int minValue, int maxValue)
        {
            lock (sync) return base.Next(minValue, maxValue);
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer"></param>
        public override void NextBytes(byte[] buffer)
        {
            lock (sync) base.NextBytes(buffer);
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0.
        /// </summary>
        /// <returns></returns>
        public override double NextDouble()
        {
            lock (sync) return base.NextDouble();
        }

    }

    /// <summary>
    /// 模拟np操作库，实现数据处理相关功能
    /// </summary>
    public class NP
    {
        static ThreadSafeRandom _random = new ThreadSafeRandom();

        internal static double NextDouble()
        {
            return _random.NextDouble();
        }

    }
}
