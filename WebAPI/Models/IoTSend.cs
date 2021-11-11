namespace WebAPI.Models
{
    public class IoTSend
    {
        public string cmd { get; set; }
        public long EUI { get; set; }
        public int port{ get; set; }
        public bool confirmed { get; set; }
        public string data { get; set; }
    }
}