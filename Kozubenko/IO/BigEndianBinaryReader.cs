using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.IO
{
    public class BigEndianBinaryReader
    {
        int CurIndex = 0;
        public List<byte> Data = new List<byte>();

        public BigEndianBinaryReader(IEnumerable<byte> Data)
        {
            this.Data = new List<byte>(Data);
        }

        public byte ReadByte()
        {
            return Data[CurIndex++];
        }

        public ushort ReadUInt16()
        {
            int msb = ReadByte();
            var lsb = ReadByte();

            return (ushort)((msb << 8) | lsb);
        }

        public uint ReadUInt32()
        {
            int msb = ReadByte();
            int msb1 = ReadByte();
            int msb2 = ReadByte();
            var lsb = ReadByte();

            return (uint)((msb << 24) | (msb1 << 16) | (msb2 << 8) | lsb);
        }

        public byte[] ReadBytes(int length)
        {
            var list = new List<byte>();

            for (int i = 0; i < length; i++)
            {
                list.Add(ReadByte());
            }

            return list.ToArray();
        }

        public int BytesToRead
        {
            get
            {
                var num = Data.Count - CurIndex;

                return num < 0 ? 0 : num;
            }
        }
    }
}
