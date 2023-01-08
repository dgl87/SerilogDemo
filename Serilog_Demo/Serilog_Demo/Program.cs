using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Serilog_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build();       // L� as defini��es do arquivo appsettings.json

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config).CreateLogger(); /* O Usamos uma instancia de LoggerConfiguration e a partir das informa��es obtidas no 
                                                                 * arquivo appsettings.json criamos um logger usando os sinks, os enrichers e as demais defini��es
                                                                 */
            try
            {
                Log.Information("API inicializando...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "A aplica��o falhou a iniciar.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()                                       // Define o Serilog como provedor de Log
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
