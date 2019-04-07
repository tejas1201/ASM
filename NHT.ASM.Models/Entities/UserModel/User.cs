using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Models.Entities.UserModel
{
    /// <summary>
    /// Entity for user details
    /// </summary>
    
    public class User: DomainEntity
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
        [Required,Range(1,int.MaxValue)]
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
        [Required,Range(1,int.MaxValue)]
        public int UserTypeId { get; set; }

        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; }

        /// <inheritdoc/>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
