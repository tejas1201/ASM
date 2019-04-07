using NHT.ASM.Dal.Interfaces;
using NHT.ASM.Infrastructure;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Dal.Repositories
{
    public class UserRepository : DomainEntityRepository<User>, IUserRepository
    {
        public UserRepository(IAsmContext context) : base(context)
        {
        }
    }
}
