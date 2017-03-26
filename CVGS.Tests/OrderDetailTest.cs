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
    class OrderDetailTest
    {
        OrderDetail orderDetail;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            orderDetail = new OrderDetail();
            context = new ValidationContext(orderDetail, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(OrderDetail), typeof(OrderDetailMetadata)), typeof(OrderDetail));
        }

        [Test]
        public void NullQuantity()
        {            
            Assert.IsNotNull(orderDetail.quantity);
        }

        [Test]
        public void NumericQuantity()
        {
            Assert.IsInstanceOf(typeof(int), orderDetail.quantity);
        }

        [Test]
        public void LargerQuantity()
        {
            orderDetail.quantity = 1000;
            Assert.IsFalse(Validator.TryValidateObject(orderDetail, context, results, true));
        }

        [Test]
        public void ValidQuantity()
        {
            orderDetail.quantity = 1;
            Assert.IsTrue(Validator.TryValidateObject(orderDetail, context, results, true));
        }

        [Test]
        public void NullUnitPrice()
        {
            Assert.IsNotNull(orderDetail.uPrice);
        }

        [Test]
        public void NumericUnitPrice()
        {
            Assert.IsInstanceOf(typeof(decimal), orderDetail.uPrice);
        }

        [Test]
        public void LargerUnitPrice()
        {
            orderDetail.uPrice = 1000.00m;
            Assert.IsFalse(Validator.TryValidateObject(orderDetail, context, results, true));
        }

        [Test]
        public void ValidUnitPrice()
        {
            orderDetail.quantity = 1;
            orderDetail.uPrice = 9.99m;
            Assert.IsTrue(Validator.TryValidateObject(orderDetail, context, results, true));
        }
    }
}