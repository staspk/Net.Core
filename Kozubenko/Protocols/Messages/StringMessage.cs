using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kozubenko.Protocols.Messages
{
    public class StringMessage
    {
        public string Message { get; set; }

        public StringMessage() { }

        public StringMessage(string Message)
        {
            this.Message = Message;
        }
        public StringMessage(IEnumerable<byte> Message)
        {
            this.Deserialize(Message);
        }

        public byte[] Serialize()
        {
            return System.Text.Encoding.UTF8.GetBytes(Message);
        }

        public void Deserialize(IEnumerable<byte> Data)
        {
            this.Message = System.Text.Encoding.UTF8.GetString(Data.ToArray());
        }
    }
}
