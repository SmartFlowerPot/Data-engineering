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

        private readonly Plant _plant;
        private readonly Account _account;

        private const string ValidUsername = "PLANT TEST USERNAME";
        private const string PlantNickname = "TEST PLANT";
        private const string ValidEui = "123465789";
        private const string InvalidEui = "696969696";

        public PlantControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            _plant = new Plant
            {
                DOB = new DateTime(2019, 09, 23),
                EUI = ValidEui,
                Nickname = PlantNickname
            };
            _account = new Account()
            {
                Username = ValidUsername,
                DateOfBirth = DateTime.Now,
                Password = "password",
                Gender = "Male"
            };
        }

        [Fact]
        public async Task GetPlantInvalidEuiTest()
        {
            //Arrange
            await DeletePlantAsync();
            await PersistPlantAsync();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/plant?eui={InvalidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
            //Clean up
            await DeletePlantAsync();
        }
        
        [Fact]
        public async Task GetPlantValidEuiTest()
        {
            //Arrange
            await DeletePlantAsync();
            await PersistPlantAsync();
            
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
            if (plant != null) Assert.Equal(PlantNickname, plant.Nickname);
            //Clean up
            await DeletePlantAsync();
        }

        [Fact]
        public async Task RemovePlantTest()
        {
            //Arrange
            await DeletePlantAsync();
            await PersistPlantAsync();
            
            //Act
            var response = await TestClient.GetAsync($"{Https}/plant?eui={ValidEui}");
            var response1 = await TestClient.DeleteAsync($"{Https}/plant?eui={ValidEui}");
            var response2 = await TestClient.GetAsync($"{Https}/plant?eui={ValidEui}");
            
            //Assert
            _testOutputHelper.WriteLine("RESPONSE 0: "+response.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("RESPONSE 1: "+response1.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("RESPONSE 2: "+response2.Content.ReadAsStringAsync().Result);
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response2.StatusCode);
            
            //Clean up
            await DeletePlantAsync();
        }

        private async Task PersistPlantAsync()
        {
            IAccountRepo accountRepo = new AccountRepo();
            await accountRepo.PostAccountAsync(_account);
            
            IPlantRepo repo = new PlantRepo();
            await repo.PostPlantAsync(_plant, ValidUsername);
        }
        
        private async Task DeletePlantAsync()
        {
            IAccountRepo accountRepo = new AccountRepo();
            await accountRepo.DeleteAccountAsync(_account.Username);
        }
    }
}