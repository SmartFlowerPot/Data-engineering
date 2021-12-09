using System;
using System.Globalization;
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
    public class TemperatureControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private Temperature _temperature;

        private const string validEui = "123465789";
        private const string invalidEui = "00000000";
        
        public TemperatureControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;

            _temperature = new()
            {
                EUI = validEui,
                TemperatureInDegrees = new decimal(23.32),
                TimeStamp = DateTime.Now
            };
        }

        [Fact]
        public void DateTimeTest()
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            _testOutputHelper.WriteLine(dateTime.ToString(CultureInfo.InvariantCulture));
            _testOutputHelper.WriteLine(DateTime.Compare(DateTime.Now, dateTime).ToString());
        }
        
        [Fact]
        public async Task GetTemperature_InvalidEUITest()
        {
            //Arrange
            await PersistTemperatureAsync();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{https}/temperature?eui={invalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Clean up
            await DeleteTemperatureAsync();
        }
        
        [Fact]
        public async Task GetTemperature_ValidEUITest()
        {
            //Arrange
            await PersistTemperatureAsync();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{https}/temperature?eui={validEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            //Clean up
            await DeleteTemperatureAsync();
        }
        
        private async Task PersistTemperatureAsync()
        {
            ITemperatureRepo repo = new TemperatureRepo();
            //await repo.PostTemperatureAsync(_temperature);
        }

        private async Task DeleteTemperatureAsync()
        {
            ITemperatureRepo repo = new TemperatureRepo();
            //await repo.DeleteTemperatureAsync(validEui);
        }
    }
}