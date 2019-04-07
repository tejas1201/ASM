using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using NHT.ASM.Helpers;

namespace NHT.ASM.Tests.Integration.Helpers
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;

        public TestServerFixture()
        {
            var assemblyName = "NHT.ASM.Api";
            IWebHostBuilder builder = new WebHostBuilder()
                .UseContentRoot(Path.Combine(ConfigHelper.GetSolutionBasePath(), assemblyName))
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("appsettings.tests.integration.json") //the file is set to be copied to the output directory if newer
                    .Build()
                ).UseStartup(assemblyName);

            _testServer = new TestServer(builder);
            
           
        }

        public void Dispose()
        {
           
            _testServer.Dispose();
        }
    }
}
