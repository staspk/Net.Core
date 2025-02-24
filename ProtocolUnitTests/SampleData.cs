using Kozubenko.Protocols;
using Kozubenko.Protocols.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolUnitTests
{
    public class SampleData
    {
        public static byte[] GenerateExampleMeterResponse()
        {
            return new byte[] { 218, 173, 0, 0, 0, 3, 0, 8, 103, 177, 101, 181, 0, 0, 9, 196 };
        }

        public static byte[] GenerateSimpleConsumptionResponse(uint consumption = 50000)
        {
            var consumptionInfo = new SimpleConsumption
            {
                Timestamp = DateTime.UtcNow,
                Consumption = consumption
            };

            var payload = consumptionInfo.ToArray();

            var response = new MyProtocol
            {
                MsgID = MyProtocolMsgID.SimpleConsumption,
                Payload = payload
            };

            return response.Serialize();
        }
    }
}
