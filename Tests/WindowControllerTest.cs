using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Tests
{
    public class WindowControllerTest : IntegrationTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly MeasurementPersistence _persistence;

        
        private const string ValidEui = "qwerty";
        private const string InvalidEui = "00000000";

        public WindowControllerTest(ITestOutputHelper outputHelper)
        {
            _testOutputHelper = outputHelper;
            _persistence = new MeasurementPersistence();
        }

        [Fact]
        public async Task ControlWindowOnValidDevice()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");
            
            //Act
            var open = await TestClient.PostAsync($"{Https}/window?eui={ValidEui}&toOpen=true",content);
            var close = await TestClient.PostAsync($"{Https}/window?eui={ValidEui}&toOpen=false",content);
            
            //Assert
            _testOutputHelper.WriteLine("OPEN REQUEST: "+open.StatusCode + " "+open.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("CLOSE REQUEST: "+close.StatusCode + " "+close.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.Accepted, open.StatusCode);
            Assert.Equal(HttpStatusCode.Accepted, close.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
        
        [Fact]
        public async Task ControlWindowOnInvalidDevice()
        {
            //Arrange
            await _persistence.DeleteMeasurement();
            await _persistence.PersistMeasurement();
            HttpContent content = new StringContent("", Encoding.UTF8, "application/json");

            //Act
            var open = await TestClient.PostAsync($"{Https}/window?eui={InvalidEui}&toOpen=true",content);
            var close = await TestClient.PostAsync($"{Https}/window?eui={InvalidEui}&toOpen=false",content);
            
            //Assert
            _testOutputHelper.WriteLine("OPEN REQUEST: "+open.StatusCode + " "+open.Content.ReadAsStringAsync().Result);
            _testOutputHelper.WriteLine("CLOSE REQUEST: "+close.StatusCode + " "+close.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.NotFound, open.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, close.StatusCode);
            
            //Clean up
            await _persistence.DeleteMeasurement();
        }
    }
}