namespace WebAPI.Models
{
    public class IoTReceive
    {
        public string cmd { get; set; }
        public long EUI { get; set; }
        public long ts { get; set; }
        public bool ack { get; set; }
        public int fcnt { get; set; }
        public int port{ get; set; }
        public string data { get; set; }
    }
}