using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHT.ASM.Infrastructure;

namespace NHT.ASM.Tests.Unit.Infrastructure
{
    public class EntityOne : DomainEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }

    [TestClass]
    public class DomainEntityTests
    {
        [TestMethod]
        public void EqualityOperator_DomainEntityToDomainEntity_IsEqual_Test()
        {
            var entityOne = new EntityOne {Id = 1, Name = "One", Age = 1};
            var entityOneTwo = entityOne;

            bool isEqual = entityOne == entityOneTwo;

            Assert.IsTrue(isEqual);
        }
        
        [TestMethod]
        public void EqualityOperator_TransientDomainEntities_IsNotEqual_Test()
        {
            var entityOne = new EntityOne {Name = "One", Age = 1};
            var entityOneTwo = new EntityOne {Name = "One", Age = 1};

            bool isNotEqual = entityOne != entityOneTwo;

            Assert.IsTrue(isNotEqual);
        }

        [TestMethod]
        public void EqualityOperator_SameInstanceOrigin_AreEqual_Test()
        {
            var entityOne = new EntityOne {Name = "One", Age = 1};
            var entityOneTwo = entityOne;

            bool isEqual = entityOne == entityOneTwo;

            Assert.IsTrue(isEqual);
        }

        

        [TestMethod]
        public void EqualityOperator_CompareToNull_IsNotEqual_Test()
        {
            var entityOne = new EntityOne {Name = "One", Age = 1};

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            bool isNotEqual = null != entityOne;

            Assert.IsTrue(isNotEqual);
        }

        [TestMethod]
        public void InEqualityOperator_DomainEntityToDomainEntity_IsNotEqual_Test()
        {
            var entityOne = new EntityOne {Id = 1,Name = "One", Age = 1};
            var entityOneTwo = new EntityOne {Id = 2, Name = "Two", Age = 2};

            bool isNotEqual = entityOne != entityOneTwo;

            Assert.IsTrue(isNotEqual);
        }
        
        [TestMethod]
        public void Equals_DomainEntityToString_IsNotEqual_Test()
        {
            var entityOne = new EntityOne {Id = 1,Name = "One", Age = 1};
            string notDomaintEntity = "Not a DomainEntity Instance;";

            // ReSharper disable once SuspiciousTypeConversion.Global
            bool isNotEqual = !entityOne.Equals(notDomaintEntity);

            Assert.IsTrue(isNotEqual);
        }
        
        [TestMethod]
        public void IsTransient_IfNoId_ReturnsTrue_Test()
        {
            var entity = new EntityOne {Name = "One", Age = 1};

            bool isTransient = entity.IsTransient();

            Assert.IsTrue(isTransient);
        }
        
        [TestMethod]
        public void GetHashCode_TwoNotTransientDomainentities_ReturnNotEqualValues_Test()
        {
            var entityOne = new EntityOne {Id = 1,Name = "One", Age = 1};
            var entityOneTwo = new EntityOne {Id = 2, Name = "Two", Age = 2};

            var hashOne = entityOne.GetHashCode();
            var hashTwo = entityOneTwo.GetHashCode();

            Assert.AreNotEqual(hashOne,hashTwo);
        }
        
        [TestMethod]
        public void GetHashCode_TwoTransientDomainentities_ReturnEqualValues_Test()
        {
            var entityOne = new EntityOne {Name = "One", Age = 1};
            var entityOneTwo = new EntityOne {Name = "One", Age = 1};

            var hashOne = entityOne.GetHashCode();
            var hashTwo = entityOneTwo.GetHashCode();

            Assert.AreNotEqual(hashOne,hashTwo);
        }
    }
}