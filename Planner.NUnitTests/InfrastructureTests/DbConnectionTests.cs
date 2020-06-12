using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Planner.Infrastructure.Data.EntityFramework;

namespace Planner.NUnitTests.InfrastructureTests
{
    [TestFixture]
    [Explicit]
    public class DbContextTests
    {
        [TestCase("Planner.BlazorServer/appsettings.json", "SQLServer")]
        public void TestPlannerDbContextConnectionWithUseSqlServer(string settingsFile, string connectionStrKey)
        {
            // Create new PlannerDbContext object using the connection string from the settings file.
            var optionsBuilder = new DbContextOptionsBuilder<PlannerDbContext>();
            var connectionString = FetchConnectionString(settingsFile, connectionStrKey);
            optionsBuilder.UseSqlServer(connectionString);
            using var context = new PlannerDbContext(optionsBuilder.Options);
            
            Assert.That(context.Database.CanConnect(), Is.True);
        }

        private static string FetchConnectionString(string settingsFile, string connectionStrKey)
        {
            var fullFilePath = Path.GetFullPath("../../../../" + settingsFile);
            
            // Create configuration object from settings file and access the connection string from the object.
            return new ConfigurationBuilder()
                .AddJsonFile(fullFilePath)
                .Build()
                .GetConnectionString(connectionStrKey);
        }
    }
}