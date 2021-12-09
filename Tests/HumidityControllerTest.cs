using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class HumidityControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private Humidity humidity;

        private const string validEui = "123465789";
        private const string invalidEui = "00000000";

        public HumidityControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            humidity = new Humidity
            {
                TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(1637153416488).DateTime,
                EUI = validEui,
                RelativeHumidity = 32
            };
        }

        [Fact]
        public async Task GetHumidity_InvalidEUITest()
        {
            //Arrange
            await PersistHumidityAsync();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{https}/humidity?eui={invalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Clean up
            await DeleteHumidityAsync();
        }
        
        [Fact]
        public async Task GetHumidity_ValidEUITest()
        {
            //Arrange
            await PersistHumidityAsync();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{https}/humidity?eui={validEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            //Clean up
            await DeleteHumidityAsync();
        }

        private async Task PersistHumidityAsync()
        {
            IHumidityRepo repo = new HumidityRepo();
            //await repo.PostHumidityAsync(humidity);
        }

        private async Task DeleteHumidityAsync()
        {
            IHumidityRepo repo = new HumidityRepo();
            //await repo.DeleteHumidityAsync(validEui);
        }
    }
}