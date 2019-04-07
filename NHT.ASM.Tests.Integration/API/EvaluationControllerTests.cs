using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHT.ASM.Tests.Integration.Helpers;

namespace NHT.ASM.Tests.Integration.API
{
    [TestClass]
    public class EvaluationControllerTests
    {
        private readonly TestServerFixture _testServerFixture = new TestServerFixture();
        
        [TestMethod]
        public async Task GetEvaluationLife_ReturnsTrue()
        {
            Assert.IsTrue(true);
        }
    }
}