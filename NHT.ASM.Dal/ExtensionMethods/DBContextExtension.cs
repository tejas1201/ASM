using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using NHT.ASM.Dal.Uow;
using NHT.ASM.Data.EntityData;
using NHT.ASM.Helpers.ExtensionMethods;

namespace NHT.ASM.Dal.ExtensionMethods
{
    public static class DbContextExtension
    {

        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this AsmContext context, DummyData dummyData = null)
        {

            if (dummyData == null) dummyData = new DummyData(null);

            //clear tables
            using (new EfUnitOfWorkFactory().Create(context))
            {
                context.UserTypes.RemoveRange(context.UserTypes);
                context.Users.RemoveRange(context.Users);
            }

            if (!context.UserTypes.HasAny())
            {
                context.UserTypes.AddRange(dummyData.UserTypes);
            }

            if (!context.Users.HasAny())
            {
                context.Users.AddRange(dummyData.Users);
            }

            context.SaveChanges();
        }

    }
}
