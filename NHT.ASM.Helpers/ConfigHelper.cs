using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;

namespace NHT.ASM.Helpers
{
    public static class ConfigHelper
    {
        /// <summary>
        /// Gets the connectionstring from the appsettings.databasestring.json file (see Solution Items) in the solution root
        /// </summary>
        /// <param name="solutionBasePath">Optional to not auto resolve the solution base path</param>
        /// <returns></returns>
        public static string GetConnectionString(string solutionBasePath = null)
        {
            //To add the connection string to the Windows Environment Variables execute the following command in the command prompt
            //setx CUSTOMCONNSTR_EvaluatorContextDb "Server=.\MYSERVER;Database=DbName;Trusted_Connection=True;ConnectRetryCount=0"
            //This should return a success message
            //more info here: https://andrewlock.net/how-to-set-the-hosting-environment-in-asp-net-core/
            //how to set it on IIS on the server: https://stackoverflow.com/a/36836533/1343595
            var environmentString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_AsmContextDb");

            if (!string.IsNullOrEmpty(environmentString))
                return environmentString;

            if (!string.IsNullOrEmpty(solutionBasePath))
                return GetStringFromConfig(Path.Combine(solutionBasePath, "asm.databasestring.json"));

            var filePath = Path.Combine(GetSolutionBasePath(), "asm.databasestring.json");

            return GetStringFromConfig(filePath);
        }

        private static string GetStringFromConfig(string filePath)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile(filePath) //you can change the value of the connectionstring in the appsettings file and add it to gitignore so the change will not effect others
                .Build();

            var connectionString = config.GetConnectionString("AsmContextDb");
            return connectionString;
        }

        /// <summary>
        /// Gets the current soution base path
        /// </summary>
        /// <returns></returns>
        public static string GetSolutionBasePath()
        {
            var appPath = PlatformServices.Default.Application.ApplicationBasePath;
            var binPosition = appPath.IndexOf("\\bin", StringComparison.Ordinal);
            var basePath = appPath.Remove(binPosition);

            var backslashPosition = basePath.LastIndexOf("\\", StringComparison.Ordinal);
            basePath = basePath.Remove(backslashPosition);
            return basePath;
        }
    }
}