using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.Utils
{
    public static class Random
    {
        public static uint GenerateRandomUInt()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static int NextInt(int min, int max)
        {
            System.Random random = new System.Random();
            return random.Next(min, max);
        }
    }
}
