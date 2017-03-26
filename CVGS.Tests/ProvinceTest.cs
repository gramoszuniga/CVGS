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
    class ProvinceTest
    {
        Province prov;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            prov = new Province();
            prov.provinceCode = "Ontario";
            context = new ValidationContext(prov, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Province), typeof(ProvinceMetadata)), typeof(Province));
        }

        [Test]
        public void NullProvinceCode()
        {
            prov.provinceCode = null;
            Assert.IsFalse(Validator.TryValidateObject(prov, context, results, true));
        }

        [Test]
        public void EmptyProvinceCode()
        {
            prov.provinceCode = "";
            Assert.IsFalse(Validator.TryValidateObject(prov, context, results, true));
        }

        [Test]
        public void LargerProvinceCode()
        {
            prov.provinceCode = "ThisLargeNameIsWayMoreThanFiftyCharactersButINeedMoreLetterInThisString";
            Assert.IsFalse(Validator.TryValidateObject(prov, context, results, true));
        }

        [Test]
        public void ValidProvinceCode()
        {
            Assert.IsTrue(Validator.TryValidateObject(prov, context, results, true));
        }

        [Test]
        public void NullProvincialTax()
        {
            Assert.IsNotNull(prov.provTax);
        }

        [Test]
        public void NumericProvincialTax()
        {
            Assert.IsInstanceOf(typeof(decimal), prov.provTax);
        }

        [Test]
        public void LargerProvincialTax()
        {
            prov.provTax = 1.01m;
            Assert.IsFalse(Validator.TryValidateObject(prov, context, results, true));
        }

        [Test]
        public void ValidProvincialTax()
        {
            prov.provTax = 0.13m;
            Assert.IsTrue(Validator.TryValidateObject(prov, context, results, true));
        }
    }
}