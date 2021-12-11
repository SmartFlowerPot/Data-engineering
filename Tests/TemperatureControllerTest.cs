using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class TemperatureControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MeasurementPersistence _persistence;

        private const string ValidEui = "qwerty";
        private const string InvalidEui = "00000000";
        
        public TemperatureControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            _persistence = new MeasurementPersistence();
        }

        [Fact]
        public async Task PopulateDB()
        {
            await _persistence.PersistMeasurement();
        }
        
        [Fact]
        public async Task CleanUpDB()
        {
            await _persistence.DeleteMeasurement();
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
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{Https}/temperature?eui={InvalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task GetTemperature_ValidEUITest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/temperature?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var temp = JsonSerializer.Deserialize<Temperature>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (temp != null) Assert.Equal(20, temp.TemperatureInDegrees);

            //Clean up
            await _persistence.DeleteMeasurement();
        }
        [Fact]
        public async Task GetTemperatureHistory()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Measurement that is older than 7 days
            var measurement = new Measurement()
            {
                CO2 = 999,
                Temperature = 999,
                Humidity = 999,
                TimeStamp = DateTime.Now.AddDays(-8)
            };
            await _persistence.PersistMeasurement(measurement);
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/temperature/history?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var temps = JsonSerializer.Deserialize<List<Temperature>>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            //Assert if all measurements are no more than 7 days old
            var dateTime = DateTime.Now.AddDays(-7);
            foreach (var isAfter in temps.Select(t => DateTime.Compare(t.TimeStamp, dateTime)))
            {
                Assert.Equal(1,isAfter);
            }
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
    }
}