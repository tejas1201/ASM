using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NHT.ASM.Infrastructure;
using NHT.ASM.Models.Entities.UserModel;

namespace NHT.ASM.Models.DataTransferObjects.UserModel
{
    /// <summary>
    /// Dto for <see cref="UserType"/>
    /// </summary>
    public class UserTypeDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the value (Admin,Secretary or Member)
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc/>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
