using CVGS.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVGSModelUnitTests
{
    [TestFixture]
    class RoleTest
    {
        Role role;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            role = new Role();
            role.roleCode = "Employee";
            context = new ValidationContext(role, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Role), typeof(RoleMetadata)), typeof(Role));
        }

        [Test]
        public void NullRoleCode()
        {
            role.roleCode = null;
            Assert.IsFalse(Validator.TryValidateObject(role, context, results, true));
        }

        [Test]
        public void EmptyRoleCode()
        {
            role.roleCode = "";
            Assert.IsFalse(Validator.TryValidateObject(role, context, results, true));
        }

        [Test]
        public void LargerRoleCode()
        {
            role.roleCode = "ThisLargeNameIsWayMoreThanFourtyCharactersButINeedMoreLetterInThisString";
            Assert.IsFalse(Validator.TryValidateObject(role, context, results, true));
        }

        [Test]
        public void ValidRoleCode()
        {
            Assert.IsTrue(Validator.TryValidateObject(role, context, results, true));
        }

        [Test]
        public void NullDiscountPercentage()
        {
            Assert.IsNull(role.disPct);
        }

        [Test]
        public void NumericDiscountPercentage()
        {
            role.disPct = 0.10m;
            Assert.IsInstanceOf(typeof(decimal), role.disPct);
        }

        [Test]
        public void LargerDiscountPercentage()
        {
            role.disPct = 1.01m;
            Assert.IsFalse(Validator.TryValidateObject(role, context, results, true));
        }

        [Test]
        public void ValidDiscountPercentage()
        {
            role.disPct = 0.10m;
            Assert.IsTrue(Validator.TryValidateObject(role, context, results, true));
        }
    }
}