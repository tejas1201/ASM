using AutoMapper;
using NHT.ASM.Models.DataTransferObjects.UserModel;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Bll.ConfigurationHelpers
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
            : this("AsmProfile")
        {
        }
        protected AutoMapperProfileConfiguration(string profileName)
            : base(profileName)
        {

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
