using Kozubenko.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.Protocols
{
    public enum MyProtocolMsgID
    {
        Unknown = 0,
        UnixTime = 1,
        SimpleConsumption = 2,

        /// <summary>
        /// UTF-8 String
        /// </summary>
        StringMessage = 3
    }

    public class MyProtocol
    {
        public const ushort MAGIC_NUMBER = 0xDAAD;   //     0xDAAD     =>   55981    =>  1101 1010 1010 1101

        public int SequenceNum { get; set; } = 1;
        public MyProtocolMsgID MsgID { get; set; }

        public byte[] Payload { get; set; }

        public string ParseError { get; set; } = null;
        public bool IsValid => ParseError == null;

        public MyProtocol() { }
        public MyProtocol(byte[] ProtocolMessage)
        {
            Deserialize(ProtocolMessage);
        }

        void Deserialize(byte[] ProtocolMessage)
        {
            try
            {
                var reader = new BigEndianBinaryReader(ProtocolMessage);
                var firstHeader = reader.ReadUInt16();

                if (firstHeader != MAGIC_NUMBER)
                {
                    ParseError = $"Unexpected header value: {firstHeader}";
                    return;
                }

                SequenceNum = reader.ReadUInt16();
                MsgID = (MyProtocolMsgID)reader.ReadUInt16();
                var len = reader.ReadUInt16();

                if (len > 0)
                {
                    Payload = reader.ReadBytes(len);
                }
            }
            catch (Exception e)
            {
                ParseError = e.Message;
            }
        }

        public byte[] Serialize()
        {
            var writer = new BigEndianBinaryWriter();
            writer.WriteUInt16(MAGIC_NUMBER);
            writer.WriteUInt16(SequenceNum);
            writer.WriteUInt16((int)MsgID);

            if (Payload != null)
            {
                writer.WriteUInt16(Payload.Length);
                writer.WriteRange(Payload);
            }
            else
                writer.WriteUInt16(0);

            return writer.ToArray();
        }

        public void IncrementSequence() => SequenceNum++;
    }
}
