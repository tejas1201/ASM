using System;
using Microsoft.EntityFrameworkCore;
using NHT.ASM.Dal;
using NHT.ASM.Helpers;

namespace NHT.ASM.Tests.Database
{
    public class EvaluatorContextOptions
    {
        public static DbContextOptions<AsmContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<AsmContext>()
                .UseSqlServer(ConfigHelper.GetConnectionString() ?? throw new InvalidOperationException()).Options;
            
            return options;
        }
    }
}
