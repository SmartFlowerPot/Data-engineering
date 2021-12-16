using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class LightControllerTest: IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MeasurementPersistence _persistence;
        
        private const string ValidEui = "qwerty";
        private const string InvalidEui = "00000000";

        public LightControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            _persistence = new MeasurementPersistence();
        }

        [Fact]
        public async Task GetLight_InvalidEUITest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{Https}/light?eui={InvalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task GetLight_ValidEUITest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/light?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var light = JsonSerializer.Deserialize<Light>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (light != null) Assert.Equal(12, light.LightLevel);

            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task GetLightHistory()
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
                Light = 999,
                TimeStamp = DateTime.Now.AddDays(-8)
            };
            await _persistence.PersistMeasurement(measurement);
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/light/history?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var temps = JsonSerializer.Deserialize<List<Light>>(json, new JsonSerializerOptions()
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