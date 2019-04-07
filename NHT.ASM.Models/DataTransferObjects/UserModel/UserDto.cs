using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NHT.ASM.Infrastructure;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Models.DataTransferObjects.UserModel
{
    /// <summary>
    /// Dto for <see cref="User"/>
    /// </summary>
    public class UserDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the User name
        /// </summary>
        [Required, Range(1, int.MaxValue)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Isactive flag
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the Email Id
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the User Type Id
        /// </summary>
        [Required, Range(1, int.MaxValue)]
        public int UserTypeId { get; set; }

        /// <summary>
        /// Gets or sets the user type
        /// </summary>
        public UserTypeDto UserType { get; set; }

        /// <inheritdoc/>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
