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
    namespace Tests
    {
        public class CO2ControllerTest : IntegrationTest
        {
            private readonly ITestOutputHelper _testOutputHelper;
            private MeasurementPersistence _persistence;

            private const string ValidEui = "qwerty";
            private const string InvalidEui = "00000000";

            public CO2ControllerTest(ITestOutputHelper outputHelper)
            {
                _testOutputHelper = outputHelper;
                _persistence = new MeasurementPersistence();
            }


            [Fact]
            public async Task GetValidCO2Test()
            {
                //Arrange
                await _persistence.DeleteMeasurement();
                await _persistence.PersistMeasurement();

                //Act
                HttpResponseMessage response = await TestClient.GetAsync($"{Https}/CO2?eui={ValidEui}");
                var json = await response.Content.ReadAsStringAsync();
                var coTwo = JsonSerializer.Deserialize<COTwo>(json, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                //Assert
                _testOutputHelper.WriteLine("RESPONSE: " + response.Content.ReadAsStringAsync().Result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                if (coTwo != null) Assert.Equal(420, coTwo.CO2Level);

                //Clean up
                await _persistence.DeleteMeasurement();
            }

            [Fact]
            public async Task GetInvalidCO2Test()
            {
                //Arrange
                await _persistence.DeleteMeasurement();
                await _persistence.PersistMeasurement();

                //Act
                HttpResponseMessage response = await TestClient.GetAsync($"{Https}/CO2?eui={InvalidEui}");

                //Assert
                _testOutputHelper.WriteLine("RESPONSE: " + response.Content.ReadAsStringAsync().Result);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

                //Clean up
                await _persistence.DeleteMeasurement();
            }
            
            [Fact]
            public async Task GetCO2History()
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
                var response = await TestClient.GetAsync($"{Https}/co2/history?eui={ValidEui}");
                var json = await response.Content.ReadAsStringAsync();
                var temps = JsonSerializer.Deserialize<List<COTwo>>(json, new JsonSerializerOptions()
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
}