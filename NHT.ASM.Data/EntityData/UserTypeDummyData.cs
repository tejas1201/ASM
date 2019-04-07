using System;
using System.Collections.Generic;
using System.Text;
using NHT.ASM.Models.Entities.UserModel;
using NHT.ASM.Models.Enums;

namespace NHT.ASM.Data.EntityData
{
    public partial class DummyData
    {
        public List<UserType> UserTypes { get; set; }

        private void AddUserType(int id, string value)
        {
            var userType = new UserType
            {
                Id = id,
                Value = value
            };

            UserTypes.Add(userType);
        }

        /// <summary>
        /// Creates the <see cref="UserTypes"/>
        /// </summary>
        private void SetUserTypes()
        {
            UserTypes = new List<UserType>();
            AddUserType(UserTypeEnum.Admin.Id, UserTypeEnum.Admin.Value);
            AddUserType(UserTypeEnum.Secretary.Id, UserTypeEnum.Secretary.Value);
            AddUserType(UserTypeEnum.Member.Id, UserTypeEnum.Member.Value);
        }
    }
}
