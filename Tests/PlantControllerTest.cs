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
    public class PlantControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private Plant plant;

        private const string validEui = "123465789";
        private const string invalidEui = "696969696";

        public PlantControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            plant = new Plant
            {
                DOB = new DateTime(2019, 09, 23),
                EUI = validEui,
                Nickname = "Alex's plant"
            };
        }

        [Fact]
        public async Task GetPlantInvalidEUITest()
        {
            //Arrange
            await PersistPlantAsync();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{https}/plant?eui={invalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            //Clean up
            await DeletePlantAsync();
        }
        
        [Fact]
        public async Task GetPlantValidEUITest()
        {
            //Arrange
            await PersistPlantAsync();
            
            //Act
            HttpResponseMessage response = await TestClient.GetAsync($"{https}/plant?eui={validEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            //Clean up
            await DeletePlantAsync();
        }

        private async Task PersistPlantAsync()
        {
            IPlantRepo repo = new PlantRepo();
            await repo.PostPlantAsync(plant, "lucas");
        }

        private async Task DeletePlantAsync()
        {
            IPlantRepo repo = new PlantRepo();
            await repo.DeletePlantAsync(validEui);
        }
    }
}