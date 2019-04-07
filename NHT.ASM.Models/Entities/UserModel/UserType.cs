using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Models.Entities.UserModel
{
    /// <summary>
    /// Entity for User Type
    /// </summary>
    public class UserType : DomainEntity
    {
        /// <summary>
        /// Gets or sets the unique Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None), Display(AutoGenerateField = false)]
        public override int Id { get; set; }

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
