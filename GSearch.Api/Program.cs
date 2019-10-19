using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace GSearch.Api
{
    public class Program
    {
        public static string Env { get; } = (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production");

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Env}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
            try
            {
                Log.Information("Starting Up...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception)
            {

                Log.Fatal("Application Startup Failed.");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).
            UseSerilog().
            ConfigureWebHostDefaults(webBuilder =>
            {

                webBuilder.UseStartup<Startup>().UseConfiguration(Configuration);
            });
    }
}
