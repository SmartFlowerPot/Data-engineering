using System;

namespace WebAPI.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public string Nickname { get; set; }
        public string DeviceIdentifier { get; set; }
    }
}