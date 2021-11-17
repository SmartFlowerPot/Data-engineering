
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebAPI.Gateway;
using WebAPI.Gateway.Service;


namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoriotClient client = new LoriotClient();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}