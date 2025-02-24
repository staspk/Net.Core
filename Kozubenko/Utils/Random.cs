using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.Utils
{
    public class Random
    {
        public static uint GenerateRandomUInt()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
    }
}
