using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NHT.ASM.Helpers;

namespace NHT.ASM.Dal
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AsmContext>
    {
        AsmContext IDesignTimeDbContextFactory<AsmContext>.CreateDbContext(string[] args)
        {
            var connectionString = ConfigHelper.GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<AsmContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AsmContext(optionsBuilder.Options);
        }

        
    }
}
