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

        public static void Stop(string name = null)
        {
            var ticks = Timer.stopwatch.ElapsedTicks;
            var ms = Timer.stopwatch.ElapsedMilliseconds;

            if (name != null)
                Console.Write($"{name} ");

            if (ms < 10)
                Console.WriteLine($"Timer Elapsed: {ticks} ticks");
            else if (ms < 10000)
                Console.WriteLine($"Timer Elapsed: {ms}ms");
            else
                Console.WriteLine($"Timer Elapsed: {ms / 1000}s");
        }
    }
}
