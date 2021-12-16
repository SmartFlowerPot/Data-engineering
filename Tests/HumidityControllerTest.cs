using System;
using System.Collections.Generic;
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
    public class HumidityControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MeasurementPersistence _persistence;
        
        private const string ValidEui = "qwerty";
        private const string InvalidEui = "00000000";

        public HumidityControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            _persistence = new MeasurementPersistence();
        }

        [Fact]
        public async Task GetHumidity_InvalidEUITest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{Https}/humidity?eui={InvalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task GetHumidity_ValidEUITest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/humidity?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var humidity = JsonSerializer.Deserialize<Humidity>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (humidity != null) Assert.Equal(45, humidity.RelativeHumidity);

            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task GetHumidityHistory()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Measurement that is older than one week
            var measurement = new Measurement()
            {
                CO2 = 999,
                Temperature = 999,
                Humidity = 999,
                TimeStamp = DateTime.Now.AddDays(-8)
            };
            await _persistence.PersistMeasurement(measurement);
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/humidity/history?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var temps = JsonSerializer.Deserialize<List<Humidity>>(json, new JsonSerializerOptions()
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