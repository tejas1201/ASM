using Microsoft.EntityFrameworkCore;
using NHT.ASM.Dal.ExtensionMethods;
using NHT.ASM.Data.EntityData;

namespace NHT.ASM.Dal
{
    public static class InMemoryDatabaseContext
    {
        /// <summary>
        /// Creates an in memory database
        /// As long as database stays alive, it will retain state for the given name
        /// </summary>
        /// <param name="inMemoryDatabaseName">The name of the in memory database</param>
        /// <param name="ensureSeeded"></param>
        /// <param name="dummyData">Optional <see cref="DummyData"/> instance</param>
        /// <returns></returns>
        public static AsmContext GetEvaluatorContextContext(string inMemoryDatabaseName, bool ensureSeeded = true, DummyData dummyData = null)
        {
            DbContextOptions<AsmContext> options = new DbContextOptionsBuilder<AsmContext>()
                .UseInMemoryDatabase(inMemoryDatabaseName)
                .Options;
            var context= new AsmContext(options);
            if(ensureSeeded)
                context.EnsureSeeded(dummyData);
            return context;
        }
    }

}
