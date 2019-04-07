using System;
using System.Collections.Generic;
using System.Text;

namespace NHT.ASM.Models.Enums
{
    /// <summary>
    /// Enum for defining user type
    /// </summary>
    public class UserTypeEnum : SmartEnum<UserTypeEnum>
    {
        public static UserTypeEnum Admin = new UserTypeEnum("Administrator", 1);
        public static UserTypeEnum Secretary = new UserTypeEnum("Secretary", 2);
        public static UserTypeEnum Member = new UserTypeEnum("Member", 3);

        protected UserTypeEnum(string value, int id) : base(value, id)
        {
        }
    }
}
