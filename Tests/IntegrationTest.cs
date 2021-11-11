using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI;
using WebAPI.DataAccess;

namespace Tests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected string http = "http://localhost:5000";
        protected string https = "https://localhost:5001";
        
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(Database));
                        services.AddDbContext<Database>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB");
                        });
                    });
                });
            TestClient = appFactory.CreateClient();
        }
    }
}