using System;
using System.Collections.Generic;
using System.Text;
using NHT.ASM.Models.Entities.UserModel;
using NHT.ASM.Models.Enums;

namespace NHT.ASM.Data.EntityData
{
    public partial class DummyData
    {
        public List<User> Users { get; set; }


        private void AddUser(string fname, string lname, string uname, string password, bool isActive, string emailId, int userTypeId)
        {
            var user = new User
            {
                FirstName = fname,
                LastName = lname,
                UserName = uname,
                Password = password,
                IsActive = isActive,
                EmailId = emailId,
                UserTypeId = userTypeId
            };

            if (StartId.HasValue)
                user.Id = Users.Count + StartId.Value;

            Users.Add(user);
        }

        /// <summary>
        /// Creates the <see cref="User"/>
        /// </summary>
        private void SetUser()
        {
            Users = new List<User>();

            AddUser("Administrator", "ASM", "admin", "Passw0rd", true, "trk.thescorpian@gmail.com", UserTypeEnum.Admin.Id);
            AddUser("Secratory", "ASM", "secratory", "Passw0rd", true, "trk.thescorpian@gmail.com", UserTypeEnum.Secretary.Id);
            AddUser("Member", "ASM", "member", "Passw0rd", true, "trk.thescorpian@gmail.com", UserTypeEnum.Member.Id);
        }
    }
}
