using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHT.ASM.Infrastructure
{
    /// <summary>
    /// Serves as the base class for all entities in the system.
    /// </summary>
    public abstract class DomainEntity : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the unique ID of the entity in the underlying data store.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Display(AutoGenerateField = false)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Creation date of record
        /// </summary>
        /// <remarks>See https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute </remarks>
        [Editable(false), Display(Name = "Created", AutoGenerateField = false), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Modification date of record
        /// </summary>
        /// <remarks>See https://www.learnentityframeworkcore.com/configuration/data-annotation-attributes/databasegenerated-attribute </remarks>
        [Editable(false), Display(Name = "Last modified", AutoGenerateField = false), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Checks if the current domain entity has an identity.
        /// </summary>
        /// <returns>True if the domain entity is transient (i.e. has no identity yet), false otherwise.</returns>
        /// <remarks>No longer can we use default value of T or int, as in .NET Core a transient Id is negative int.MinValue
        /// (if a set of entities is added, the next is one higher)</remarks>
        public bool IsTransient()
        {
            return Id <= default(int);
        }

        /// <summary>
        /// Determines whether the specified int is equal to the current int.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The object to compare with the current object. 
        /// </param>
        public override bool Equals(object obj)
        {
            if (!(obj is DomainEntity))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var item = (DomainEntity)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            return item.Id.Equals(Id);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current int.
        /// </returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
            }
            return base.GetHashCode();
        }

        /// <summary>
        /// Compares two instances for equality.
        /// </summary>
        /// <param name="left">The left instance to compare.</param>
        /// <param name="right">The right instance to compare.</param>
        /// <returns>True when the objects are the same, false otherwise.</returns>
        public static bool operator ==(DomainEntity left, DomainEntity right)
        {
            if (Equals(left, null))
                return Equals(right, null);

            return left.Equals(right);
        }

        /// <summary>
        /// Compares two instances for inequality.
        /// </summary>
        /// <param name="left">The left instance to compare.</param>
        /// <param name="right">The right instance to compare.</param>
        /// <returns>False when the objects are the same, true otherwise.</returns>
        public static bool operator !=(DomainEntity left, DomainEntity right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether this object is valid or not.
        /// </summary>
        /// <param name="validationContext">Describes the context in which a validation check is performed.</param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
