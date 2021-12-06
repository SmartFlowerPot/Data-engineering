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

        private IoTMessage _message;

        public MessageProcessorTest(ITestOutputHelper testOutputHelper)
        {
            _processor = new MessageProcessor();
            _testOutputHelper = testOutputHelper;
            _message = new IoTMessage
            {
                EUI = "0004A30B00251001",
                data = "18562005d4",
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
            
            _testOutputHelper.WriteLine(temp.ToString());
            _testOutputHelper.WriteLine(humidity.ToString());
            _testOutputHelper.WriteLine(co2.ToString());
        }
        
    }
}