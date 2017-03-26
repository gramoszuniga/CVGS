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
    class OrderTest
    {
        Order order;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            order = new Order();
            order.status = "Pending";
            context = new ValidationContext(order, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Order), typeof(OrderMetadata)), typeof(Order));
        }

        [Test]
        public void NullOrderDate()
        {
            Assert.IsNotNull(order.ordDate);
        }

        [Test]
        public void EarlierOrderDate()
        {
            order.ordDate = new DateTime(2015, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void ValidOrderDate()
        {
            order.ordDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void NullStatus()
        {
            order.status = null;
            Assert.IsFalse(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void EmptyStatus()
        {
            order.status = "";
            Assert.IsFalse(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void InvalidStatus()
        {
            order.status = "Rejected";
            Assert.IsFalse(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void ValidStatus()
        {
            order.ordDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void NullTotal()
        {
            Assert.IsNotNull(order.total);
        }

        [Test]
        public void NumericTotal()
        {
            Assert.IsInstanceOf(typeof(decimal), order.total);
        }

        [Test]
        public void LargerTotal()
        {
            order.total = 1000.00m;
            Assert.IsFalse(Validator.TryValidateObject(order, context, results, true));
        }

        [Test]
        public void ValidTotal()
        {
            order.ordDate = DateTime.Today;
            order.total = 9.99m;
            Assert.IsTrue(Validator.TryValidateObject(order, context, results, true));
        }
    }
}