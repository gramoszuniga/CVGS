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
    class AccountTest
    {
        Account account;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            account = new Account();
            account.userName = "gramosz05";
            account.password = "Conestoga1234+";
            context = new ValidationContext(account, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Account), typeof(AccountMetadata)), typeof(Account));
        }

        [Test]
        public void NullUserName()
        {
            account.userName = null;
            Assert.IsFalse(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void EmptyUserName()
        {
            account.userName = "";
            Assert.IsFalse(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void LargerUserName()
        {
            account.userName = "ThisLargeNameIsWayMoreThanTenCharacters";
            Assert.IsFalse(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void ValidUserName()
        {
            account.userName = "gramosz05";
            Validator.TryValidateObject(account, context, results, true);
            Assert.IsTrue(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void NullPassword()
        {
            account.password = null;
            Assert.IsFalse(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void EmptyPassword()
        {
            account.userName = "";
            Assert.IsFalse(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void LargerPassword()
        {
            account.password = "Conestogaconestogaconestogaconestogaconestoga1234+";
            Assert.IsFalse(Validator.TryValidateObject(account, context, results, true));
        }

        [Test]
        public void ValidPassword()
        {
            account.password = "Conestoga1234+";
            Assert.IsTrue(Validator.TryValidateObject(account, context, results, true));
        }
    }
}