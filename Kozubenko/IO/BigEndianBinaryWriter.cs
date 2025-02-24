using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.IO
{
    /// <summary>
    /// Note: 2500 ==> 0x000009C4 == 32-bit representation, 0x9C4 == 16-bit reprsentation
    /// 
    /// Assume incoming value: 2500
    ///     WriteUInt32 returns => [0x00 0x00 0x09 0xc4], made up of individual bytes.
    ///            decimal repr => [   0    0    9  196]
    ///            binary  repr => 1001 1100 0100
    ///                  
    ///     To get original value back, add right side up:
    ///         9 * 16^2 =  9 * 256 = 2304
    ///        12 * 16^1 = 12 * 16  =  192
    ///         4 * 16^0 =  4 * 1   =    4
    ///                             = 2500
    ///     
    ///  Assume incoming value: 1739664309
    ///     WriteUInt32 returns => [0x67 0xB1 0x2B 0xB5], made up of individual bytes => 0x67B12BB5
    ///            decimal repr => [ 103  177   43  181]
    ///            binary  repr => 0110 0111 1011 0001 0010 1011 1011 0101
    ///
    ///    First byte:  0xB5  =>
    ///            5  * 16^0  = 5
    ///           11  * 16^1  = 176
    ///           176 + 5     => 181
    /// 
    ///    Second byte: 0x09  =>
    ///            11 * 16^0  = 11
    ///             2 * 16^1  = 32
    ///                      += 43
    ///            43 * 16^2  => 11,008
    ///      
    ///    Third byte: 0xB1  =>
    ///            1 * 16^0  = 1
    ///           11 * 16^1  = 176
    ///                     += 177
    ///           177 * 16^4 => 11,599,872
    ///           
    ///    Fourth byte: 0x67 =>
    ///            7 * 16^0 = 7
    ///            6 * 16^1 = 96
    ///                    += 103
    ///          103 * 16^6 => 1,728,053,248
    ///                                                 1739664309
    ///     1,728,053,248 + 11,599,872 + 11,008 + 181 = 1,739,664,309    CHECKS OUT
    /// </summary>
    public class BigEndianBinaryWriter
    {
        List<byte> Buffer = new List<byte>();

        public void WriteUInt32(uint num)
        {
            Buffer.Add((byte)(num >> 24));
            Buffer.Add((byte)(num >> 16));
            Buffer.Add((byte)(num >> 8));
            Buffer.Add((byte)(num));
        } 


        public void WriteUInt16(int num)
        {
            // Example 0x1234
            Buffer.Add((byte)(num >> 8));
            Buffer.Add((byte)(num));
        }
        public void WriteRange(IEnumerable<byte> items)
        {
            Buffer.AddRange(items);
        }

        public byte[] ToArray()
        {
            return Buffer.ToArray();
        }
    }
}
