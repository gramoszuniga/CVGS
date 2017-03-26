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
    class GameDetailTest
    {
        GameDetail gameDetail;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            gameDetail = new GameDetail();
            context = new ValidationContext(gameDetail, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(GameDetail), typeof(GameDetailMetadata)), typeof(GameDetail));
        }

        [Test]
        public void NullPhysicalCopy()
        {
            Assert.IsNotNull(gameDetail.phyCopy);
        }

        [Test]
        public void NullPrice()
        {
            Assert.IsNotNull(gameDetail.price);
        }

        [Test]
        public void NumericPrice()
        {
            Assert.IsInstanceOf(typeof(decimal), gameDetail.price);
        }

        [Test]
        public void LargerPrice()
        {
            gameDetail.price = 1000.00m;
            Assert.IsFalse(Validator.TryValidateObject(gameDetail, context, results, true));
        }

        [Test]
        public void ValidPrice()
        {
            gameDetail.price = 9.99m;
            Assert.IsTrue(Validator.TryValidateObject(gameDetail, context, results, true));
        }

        [Test]
        public void NullQOH()
        {
            Assert.IsNull(gameDetail.qoh);
        }

        [Test]
        public void NumericQOH()
        {
            gameDetail.qoh = 7;
            Assert.IsInstanceOf(typeof(int), gameDetail.qoh);
        }

        [Test]
        public void LargerQOH()
        {
            gameDetail.qoh = 1000;
            Assert.IsFalse(Validator.TryValidateObject(gameDetail, context, results, true));
        }

        [Test]
        public void ValidQOH()
        {
            gameDetail.qoh = 7;
            Assert.IsTrue(Validator.TryValidateObject(gameDetail, context, results, true));
        }
    }
}