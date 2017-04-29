using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace PhonebookAPITests
{
    // based on https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing#integration-testing
    class FunctionalFixture<Startup> : IDisposable
    {
        private const string SolutionName = "PhonebookAPITests.sln";
        private readonly TestServer server;
        public FunctionalFixture() : this(Path.Combine(""))
        {

        }
        public void Dispose()
        {
            Client.Dispose();
            server.Dispose();
        }
        protected FunctionalFixture(string dir)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(dir, assembly);

            var builder = new WebHostBuilder()
               .UseContentRoot(contentRoot)
               .ConfigureServices(InitializeServices)
               .UseEnvironment("Development")
               .UseStartup(typeof(Startup));

            server = new TestServer(builder);

            Client = server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");

        }
        public HttpClient Client { get;  }
        protected virtual void InitializeServices(IServiceCollection services)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;

            // Inject a custom application part manager. Overrides AddMvcCore() because that uses TryAdd().
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));

            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            manager.FeatureProviders.Add(new ViewComponentFeatureProvider());

            services.AddSingleton(manager);

        }
        private static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            // Find the folder which contains the solution file. We then use this information to find the target
            // project which we want to test.
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, SolutionName));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }

    }
}
