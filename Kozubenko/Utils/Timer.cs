using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.Utils
{
    public class Timer
    {
        private static Stopwatch stopwatch;

        public static void Start(string name = null)
        {
            if (name != null)
            {
                Console.WriteLine($"{name} Timer Started...");
            }

            Timer.stopwatch = Stopwatch.StartNew();
        }

        /// <summary>
        ///     1 tick == 100ns
        ///     1ms == 10,000 ticks == 1,000,000ns
        /// </summary>
        public static long Stop(string name = null)
        {
            long ticks = Timer.stopwatch.ElapsedTicks;

            double elapsedSeconds = (double)ticks / Stopwatch.Frequency;

            if (elapsedSeconds < 10)
                Console.WriteLine($"Timer Elapsed: {ticks}ticks  ==  {elapsedSeconds * 1000:F4}ms");
            else if(elapsedSeconds < 100)
                Console.WriteLine($"Timer Elapsed: {elapsedSeconds}s");
            else
                Console.WriteLine($"Timer Elapsed: {elapsedSeconds / 60:F2}m");

            return ticks;
        }
    }
}
