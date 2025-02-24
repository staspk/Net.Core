using Kozubenko.IO;

namespace Kozubenko.Protocols.Messages
{
    public class UnixTimeMessage
    {
        public DateTime UnixTime = DateTime.UtcNow;

        public UnixTimeMessage() { }
        public UnixTimeMessage(IEnumerable<byte> Data)
        {
            this.Deserialize(Data.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Timestamp">Seconds since 1970</param>
        public UnixTimeMessage(UInt32 Timestamp)
        {
            UnixTime = Utils.DateTime.ToUtcDateTime(Timestamp);
        }

        /// <summary>
        /// Converts DateTime into 4 bytes, big endian unixtimestamp
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            var timestamp = Utils.DateTime.ToUnixTimestamp(UnixTime);

            var writer = new BigEndianBinaryWriter();

            writer.WriteUInt32(timestamp);

            return writer.ToArray();
        }

        public void Deserialize(byte[] Data)
        {
            if (Data.Length >= 4)
            {
                var reader = new BigEndianBinaryReader(Data);

                var timestamp = reader.ReadUInt32();

                UnixTime = Utils.DateTime.ToUtcDateTime(timestamp);
            }
        }

        public UInt32 ToTimestamp()
        {
            return Utils.DateTime.ToUnixTimestamp(UnixTime);
        }
    }
}
