using System;
using WebAPI.Gateway.Model;
using WebAPI.Gateway.Service;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class MessageProcessorTest
    {
        private MessageProcessor _processor;
        
        private readonly ITestOutputHelper _testOutputHelper;

        private IoTMessage _message, _message2;

        public MessageProcessorTest(ITestOutputHelper testOutputHelper)
        {
            _processor = new MessageProcessor();
            _testOutputHelper = testOutputHelper;
            _message = new IoTMessage
            {
                EUI = "18061c0226002f",
                data = "17392602260037",
                //Random timestamp
                ts = 1638792089900,
                port = 1
            };
            
            _message2 = new IoTMessage
            {
                EUI = "0004A30B00251001",
                data = "17352802260037",
                ts = 1638792089900,
                port = 1
            };
            
            
        }
        
        [Fact]
        public void MessageProcessingTest()
        {
            var temp = _processor.CreateTemperature(_message);
            var humidity = _processor.CreateHumidity(_message);
            var co2 = _processor.CreateCo2(_message);
            var light = _processor.CreateLight(_message);
            
            _testOutputHelper.WriteLine(temp.ToString());
            _testOutputHelper.WriteLine(humidity.ToString());
            _testOutputHelper.WriteLine(co2.ToString());
            _testOutputHelper.WriteLine(light.ToString());

            _testOutputHelper.WriteLine("-------------------");
            
            temp = _processor.CreateTemperature(_message2);
            humidity = _processor.CreateHumidity(_message2);
            co2 = _processor.CreateCo2(_message2);
            light = _processor.CreateLight(_message2);
            
            _testOutputHelper.WriteLine(temp.ToString());
            _testOutputHelper.WriteLine(humidity.ToString());
            _testOutputHelper.WriteLine(co2.ToString());
            _testOutputHelper.WriteLine(light.ToString());

        }
        
    }
}