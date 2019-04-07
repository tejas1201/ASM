using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NHT.ASM.Infrastructure;
using NHT.ASM.Models.Entities;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Dal
{

    public class AsmContext : DbContext, IAsmContext
    {
        public AsmContext(DbContextOptions<AsmContext> options)
            : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public override int SaveChanges()
        {
            Audit();

            var count = ChangeTracker.Entries().Count(x => x.State == EntityState.Added);
            var result = base.SaveChanges();
            if (result < count)
            {
                throw new ArgumentException($"Saved to few entries: {result} instead of {count}");
            }

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is DomainEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((DomainEntity)entry.Entity).DateCreated = DateTime.Now;
                }
                ((DomainEntity)entry.Entity).DateModified = DateTime.Now;
            }
        }

    }
}