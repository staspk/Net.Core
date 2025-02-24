using Kozubenko.IO;

namespace Kozubenko.Protocols.Messages
{
    public class SimpleConsumption
    {
        public const int MIN_MESSAGE_LEN = 8;
        public string ParseError { get; set; } = null;
        public System.DateTime Timestamp { get; set; }
        public uint Consumption { get; set; }

        public SimpleConsumption() { }
        public SimpleConsumption(IEnumerable<byte> bytes)
        {
            var reader = new BigEndianBinaryReader(bytes);

            if (reader.BytesToRead >= MIN_MESSAGE_LEN)
            {
                var timestamp = reader.ReadUInt32();

                Timestamp = Utils.DateTime.ToUtcDateTime(timestamp);
                Consumption = reader.ReadUInt32();
            }
            else
                ParseError = $"Not enough data for SimpleConsumption message. Requires {MIN_MESSAGE_LEN}, received {reader.BytesToRead}";
        }

        public byte[] ToArray()
        {
            var writer = new BigEndianBinaryWriter();

            uint timestamp = Utils.DateTime.ToUnixTimestamp(Timestamp);

            writer.WriteUInt32(timestamp);
            writer.WriteUInt32(Consumption);

            return writer.ToArray();
        }

        public override string ToString()
        {
            return $"{Consumption}@{Timestamp:o}";
        }
    }
}
