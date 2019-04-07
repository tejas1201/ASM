using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Base Data Transfer Object to get common fields
    /// </summary>
    // ReSharper disable once InheritdocConsiderUsage
    public abstract class BaseDto: IValidatableObject
    {
        /// <summary>
        /// Gets (or sets for updating entities) the unique identifier of the entity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets the date and time the record was created in the database
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Gets the date and time the record was last modified and saved in the database
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <inheritdoc />
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
