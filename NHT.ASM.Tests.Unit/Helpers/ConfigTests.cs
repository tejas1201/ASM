using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHT.ASM.Helpers;

namespace NHT.ASM.Tests.Unit.Helpers
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void CreateDbContext_ReturnsContext()
        {
            var connectionString = ConfigHelper.GetConnectionString();
            Assert.IsFalse(string.IsNullOrEmpty(connectionString));
        }
    }
}
