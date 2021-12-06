using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class HumidityControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private Humidity humidity;

        public HumidityControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            humidity = new Humidity
            {
                TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(1637153416488).DateTime,
                EUI = "0004A30B00251001",
                RelativeHumidity = 32
            };
        }

        [Fact]
        public async Task GetHumidityTest()
        {
             
        }

        private async Task<HttpResponseMessage> PostTemperature()
        {
            string accountJson = JsonSerializer.Serialize(humidity);
            HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
            
            HttpResponseMessage responseMessage = await TestClient.PostAsync(https + "/humidity", content);
            return responseMessage; 
        }
    }
}