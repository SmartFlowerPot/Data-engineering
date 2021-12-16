using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class PlantControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MeasurementPersistence _persistence;

        private readonly Plant _plant;
        private readonly Account _account;
        
        private const string ValidEui = "qwerty";
        private const string InvalidEui = "696969696";

        public PlantControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            _persistence = new MeasurementPersistence();
        }

        [Fact]
        public async Task GetPlantInvalidEuiTest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/plant?eui={InvalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task GetPlantValidEuiTest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/plant?eui={ValidEui}");
            var json = await response.Content.ReadAsStringAsync();
            var plant = JsonSerializer.Deserialize<Plant>(json, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (plant != null) Assert.Equal("Vladimir Trump", plant.Nickname);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }

        [Fact]
        public async Task RemovePlantTest()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/plant?eui={ValidEui}");
            var response1 = await TestClient.DeleteAsync($"{Https}/plant?eui={ValidEui}");
            var response2 = await TestClient.GetAsync($"{Https}/plant?eui={ValidEui}");
            
            //Test if all measurements for that plant are also deleted 
            var response3 = await TestClient.GetAsync($"{Https}/temperature?eui={ValidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE 0: "+response.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("RESPONSE 1: "+response1.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("RESPONSE 2: "+response2.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("RESPONSE 3: "+response3.Content.ReadAsStringAsync().Result);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
    }
}