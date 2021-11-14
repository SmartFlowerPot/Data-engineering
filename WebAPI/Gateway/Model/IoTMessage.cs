using System;
using System.Collections.Generic;

namespace WebAPI.Gateway.Model
{
    public class IoTMessage
    {
        public string cmd { get; set; }
        public string EUI { get; set; }
        
        public long ts { get; set; }
        public int port { get; set; }

        public string data { get; set; }

        public List<IoTMessage> cache { get; set; }

        public override string ToString()
        {
            if (cmd.Equals("cq"))
            {
                foreach (var i in cache)
                {
                    Console.WriteLine(i);
                }
            }
            return $"cmd: {cmd}\nEUI: {EUI}\nport: {port}\nts: {ts}\ndata: {data}";
        }
    }
}