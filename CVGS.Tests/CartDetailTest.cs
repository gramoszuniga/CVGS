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
    class CartDetailTest
    {
        CartDetail cartDetail;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            cartDetail = new CartDetail();
            context = new ValidationContext(cartDetail, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(CartDetail), typeof(CartDetailMetadata)), typeof(CartDetail));
        }

        [Test]
        public void NullQuantity()
        {            
            Assert.IsNotNull(cartDetail.quantity);
        }

        [Test]
        public void NumericQuantity()
        {
            Assert.IsInstanceOf(typeof(int), cartDetail.quantity);
        }

        [Test]
        public void LargerQuantity()
        {
            cartDetail.quantity = 1000;
            Assert.IsFalse(Validator.TryValidateObject(cartDetail, context, results, true));
        }

        [Test]
        public void ValidQuantity()
        {
            cartDetail.quantity = 1;
            Assert.IsTrue(Validator.TryValidateObject(cartDetail, context, results, true));
        }
    }
}