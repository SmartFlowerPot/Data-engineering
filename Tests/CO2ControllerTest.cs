using System.Net;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;

namespace Tests
{
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
        public class CO2ControllerTest : IntegrationTest
        {
            private readonly ITestOutputHelper _testOutputHelper;
            private COTwo _co2;
            private const string validEui = "0004A30B00251001";
            private const string invalidEui = "00000000";

            public CO2ControllerTest(ITestOutputHelper outputHelper)
            {
                _testOutputHelper = outputHelper;
                _co2 = new COTwo()
                {
                    TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(1638873847).DateTime,
                    EUI = validEui,
                    CO2Level = 911
                };
            }

            private async Task<HttpResponseMessage> PostCO2()
            {
                string accountJson = JsonSerializer.Serialize(_co2);
                HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await TestClient.PostAsync(https + "/CO2", content);
                return responseMessage;
            }

            [Fact]
            public async Task GetValidCO2Test()
            {
                //Arrange
                await PersistCO2Async();
            
                //Act
                HttpResponseMessage response = await TestClient.GetAsync($"{https}/CO2?eui={validEui}");
            
                //Assert
                _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
                //Clean up
                await DeleteCO2Async();
            }
            
            [Fact]
            public async Task GetInvalidCO2Test()
            {
                //Arrange
                await PersistCO2Async();
            
                //Act
                HttpResponseMessage response = await TestClient.GetAsync($"{https}/CO2?eui={invalidEui}");
            
                //Assert
                _testOutputHelper.WriteLine("RESPONSE: "+response.Content.ReadAsStringAsync().Result);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            
                //Clean up
                await DeleteCO2Async();
            }
            
            private async Task PersistCO2Async()
            {
                ICO2Repo repo = new CO2Repo();
                //await repo.PostCO2Async(_co2);
            }

            private async Task DeleteCO2Async()
            {
                ICO2Repo repo = new CO2Repo();
                //await repo.DeleteHumidityAsync(validEui);
            }
            
            
            
        }
    }
}