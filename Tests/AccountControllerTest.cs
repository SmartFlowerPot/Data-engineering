using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class AccountControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private Account _account;
        public AccountControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _account = new Account()
            {
                Username = "AlanTuring",
                Password = "enigma"
            };
        }

        [Fact]
        public async Task GetAccount_ExistingAccount()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.GetAsync($"{Https}/account/{_account.Username}");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
        
        [Fact]
        public async Task GetAccount_NotExistingAccount()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.GetAsync($"{Https}/account/SlimShady");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);

            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
        
        [Fact]
        public async Task GetAccountLogin_CorrectUsernameAndPassword()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.GetAsync($"{Https}/account?username=AlanTuring&password=enigma");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
        
        [Fact]
        public async Task GetAccountLogin_InvalidUsernameAndPassword()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.GetAsync($"{Https}/account?username=Alan&password=enigma");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
        
        [Fact]
        public async Task GetAccountLogin_UsernameAndIncorrectPassword()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.GetAsync($"{Https}/account?username=AlanTuring&password=1234");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.BadRequest, responseMessage.StatusCode);
            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
        
        [Fact]
        public async Task PostAccount_ValidAccount()
        {
            //Arrange
            var responseMessage = await PostAccount();

            // Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            
            Assert.Equal(HttpStatusCode.Created,responseMessage.StatusCode);
            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }

        private async Task<HttpResponseMessage> PostAccount()
        {
            string accountJson = JsonSerializer.Serialize(_account);
            HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
            
            HttpResponseMessage responseMessage = await TestClient.PostAsync(Https + "/account", content);
            return responseMessage;
        }

        [Fact]
        public async Task PostAccount_TakenUsername()
        {
            // Arrange
            await PostAccount();
            string accountJson = JsonSerializer.Serialize(_account);
            HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
            
            // Act
            var responseMessage= await TestClient.PostAsync(Https + "/account", content);
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, responseMessage.StatusCode);
            
            //Clean up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
        
        [Fact]
        public async Task DeleteAccount_ExistingAccount()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.DeleteAsync($"{Https}/account/{_account.Username}");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
        }
        
        [Fact]
        public async Task DeleteAccount_NotExistingAccount()
        {
            //Arrange
            await PostAccount();
    
            //Act
            var responseMessage = await TestClient.GetAsync($"{Https}/account/SlimShady");
            
            //Assert
            _testOutputHelper.WriteLine($"UNIT TESTS: {responseMessage.StatusCode} "+responseMessage.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, responseMessage.StatusCode);
            
            // Clean Up
            await TestClient.DeleteAsync(Https + $"/account/{_account.Username}");
        }
    }
}