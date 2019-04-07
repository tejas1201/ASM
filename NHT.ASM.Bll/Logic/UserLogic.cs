using NHT.ASM.Bll.Interfaces;
using NHT.ASM.Dal.Interfaces;
using NHT.ASM.Infrastructure;
using NHT.ASM.Models.DataTransferObjects.UserModel;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Bll.Logic
{
    public class UserLogic : BaseLogic<User,UserDto>, IUserLogic
    {
        public UserLogic(IAsmContext context, IUserRepository userRepository) : base(context, userRepository)
        {
        }
    }
}
