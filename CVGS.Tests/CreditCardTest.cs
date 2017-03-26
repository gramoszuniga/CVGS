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
    class CreditCardTest
    {
        CreditCard card;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            card = new CreditCard();
            card.number = "1234123412341234";
            card.name = "Asha Farah";
            card.creditCardType = "Visa";
            card.CVV = "123";
            context = new ValidationContext(card, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(CreditCard), typeof(CreditCardMetadata)), typeof(CreditCard));
        }

        [Test]
        public void NullNumber()
        {
            card.number = null;
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void NumericNumber()
        {
            card.number = "ThisIsAnInvalidCardNumber.";
            Assert.Throws<FormatException>(() => long.Parse(card.number));
        }

        [Test]
        public void LargerNumber()
        {
            card.number = "10000000000000000";
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void ValidNumber()
        {
            card.expDate = new DateTime(2017, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void NullName()
        {
            card.name = null;
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void EmptyName()
        {
            card.name = "";
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void LargerName()
        {
            card.name = "ThisLargeNameIsWayMoreThanSixtyCharactersButINeedMoreLetterInThisString";
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void ValidName()
        {
            card.expDate = new DateTime(2017, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void NullExpirationDate()
        {
            Assert.IsNotNull(card.expDate);
        }

        [Test]
        public void InvalidExpirationDate()
        {
            card.expDate = new DateTime(2016, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void ValidExpirationDate()
        {
            card.expDate = new DateTime(2017, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void NullType()
        {
            card.creditCardType = null;
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void EmptyType()
        {
            card.creditCardType = "";
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void InvalidType()
        {
            card.creditCardType = "MasterPlop";
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void ValidType()
        {
            card.expDate = new DateTime(2017, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void NullCVV()
        {
            card.CVV = null;
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void NumericCVV()
        {
            card.CVV = "ThisIsAnInvalidCVV.";
            Assert.Throws<FormatException>(() => long.Parse(card.CVV));
        }

        [Test]
        public void LargerCVV()
        {
            card.CVV = "1000";
            Assert.IsFalse(Validator.TryValidateObject(card, context, results, true));
        }

        [Test]
        public void ValidCVV()
        {
            card.expDate = new DateTime(2017, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(card, context, results, true));
        }
    }
}