using System;
using System.Linq;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Synergy.Sample.Web.API.Extensions;
using Synergy.Sample.Web.API.Extensions.Logging;

namespace Synergy.Sample.Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Move the serilog configuration deeper - so it could be different for every env
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithEnvironmentUserName()
                        .Enrich.WithExceptionDetails()
                        .Enrich.WithProperty(EnvironmentLogProperties.ApplicationVersion, Application.GetApplicationInfo().FileVersion)
                        .Enrich.WithProperty(EnvironmentLogProperties.ApplicationName, Application.GetApplicationInfo().ProductName)
                        .WriteTo.Console()
                        .WriteTo.RollingFile(
                             new JsonFormatter(),
                             "Log/Sample-{Date}.txt",
                             fileSizeLimitBytes: 100 * 1024 * 1024,
                             retainedFileCountLimit: 5)
                        .WriteTo.Seq("http://localhost:5341")
                        .CreateLogger();

            try
            {
                Log.Information("Starting web host on machine {" + EnvironmentLogProperties.MachineName + "}");
                Program.CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration(
                     (hostingContext, config) =>
                     {
                         var environmentName = hostingContext.HostingEnvironment.EnvironmentName;
                         config.AddJsonFile($"appsettings.{environmentName}.json", true);
                         config.AddEnvironmentVariables();
                     })
                .UseServiceProviderFactory(new WindsorServiceProviderFactory())
                .ConfigureContainer<WindsorContainer>(
                     (hostBuilderContext, container) =>
                     {
                         container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
                         container.AddFacility<TypedFactoryFacility>();

                         var rootAssembly = Application.GetRootAssembly();
                         container.Register(
                             Classes
                                .FromAssemblyInThisApplication(rootAssembly)
                                .Pick()
                                .Unless(x => x.GetInterfaces().Any() == false || x.IsConstructable() == false)
                                .WithServiceAllInterfaces()
                                .LifestyleSingleton()
                             );

                         // Execute all installers in every library in the application
                         container.Install(FromAssembly.InThisApplication(rootAssembly));
                     });
    }
}