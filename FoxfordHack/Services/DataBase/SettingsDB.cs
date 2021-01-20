using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace FoxfordHack.Services.DataBase
{
    public class SettingsDB
    {
        public static readonly string ConfigurationJsonFile = "appsettings.json";
        public string GetConnectionString()
        {
                var builder = new HostBuilder();
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config
                           .AddJsonFile(ConfigurationJsonFile, true)
                           .AddEnvironmentVariables();
                });

                var host = builder.Build();
                var config = host.Services.GetService<IConfiguration>();
                return config.GetConnectionString("Default");
        }
    }
}

