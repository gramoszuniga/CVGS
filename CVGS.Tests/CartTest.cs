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
    class CartTest
    {
        Cart cart;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            cart = new Cart();
            context = new ValidationContext(cart, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Cart), typeof(CartMetadata)), typeof(Cart));
        }

        [Test]
        public void NullTotal()
        {
            Assert.IsNotNull(cart.total);
        }

        [Test]
        public void NumericTotal()
        {           
            Assert.IsInstanceOf(typeof(decimal), cart.total);
        }

        [Test]
        public void LargerTotal()
        {
            cart.total = 1000.00m;
            Assert.IsFalse(Validator.TryValidateObject(cart, context, results, true));
        }

        [Test]
        public void ValidTotal()
        {
            cart.total = 9.99m;
            Assert.IsTrue(Validator.TryValidateObject(cart, context, results, true));
        }
    }
}