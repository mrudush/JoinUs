using JoinUs.Bussiness;
using JoinUs.Interfaces;
using JoinUs.Models;
using JoinUs.Models.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Serilog;

namespace JoinUs
{
    class Program
    {
        static void Main(string[] args)
        {            
            var builder = new ConfigurationBuilder();

            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IInviteCustomerService, InviteCustomerService>();
                    services.AddTransient<IDistanceOperations, DistanceOperations>();
                    services.AddTransient<IFileOperations, FileOperations>();
                    services.AddTransient<IConfigurationValidator, ConfigurationValidator>();
                    services.Configure<ConfigurationSettings>(builder.Build().GetSection("ConfigurationSettings"));
                })  
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<InviteCustomerService>(host.Services);
            
            Log.Information("Application Started..");           

            if (! svc.Run())
            {
                Log.Information("Sorry, We couldnt invite anyone," +
                    " Please refer above for more details");                         
            }
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
                .AddEnvironmentVariables();
        }
    }
}
