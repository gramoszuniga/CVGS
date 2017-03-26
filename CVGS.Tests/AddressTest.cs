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
    class AddressTest
    {
        Address address;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            address = new Address();
            address.street = "299 Doon Valley Drive";
            address.city = "Kitchener";
            address.postalCode = "N2G 4M4";
            address.provinceCode = "Ontario";
            context = new ValidationContext(address, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Address), typeof(AddressMetadata)), typeof(Address));
        }

        [Test]
        public void NullStreet()
        {
            address.street = null;
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void EmptyStreet()
        {
            address.street = "";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void LargerStreet()
        {
            address.street = "ThisStreetExampleIsWayMoreThanOneHundredCharactersButIReallyNeedManyMoreWordsInThisExampleStringThisTime";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void ValidStreet()
        {
            address.street = "299 Doon Valley Drive";
            Assert.IsTrue(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void NullCity()
        {
            address.city = null;
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void EmptyCity()
        {
            address.city = "";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void LargerCity()
        {
            address.city = "ThisCityIsWayMoreThanSixtyCharactersButINeedMoreLetterInThisString";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void ValidCity()
        {
            address.city = "Kitchener";
            Assert.IsTrue(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void NullPostalCode()
        {
            address.postalCode = null;
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void EmptyPostalCode()
        {
            address.postalCode = "";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void LargerPostalCode()
        {
            address.postalCode = "ThisPostalCodeIsMoreThanSevenCharacters";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void InvalidPostalCode()
        {
            address.postalCode = "A1A";
            Assert.IsFalse(Validator.TryValidateObject(address, context, results, true));
        }

        [Test]
        public void ValidPostalCode()
        {
            address.postalCode = "A1A 1A1";
            Assert.IsTrue(Validator.TryValidateObject(address, context, results, true));
        }
    }
}