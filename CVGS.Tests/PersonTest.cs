using System;
using NUnit.Framework;
using CVGS.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace CVGSModelUnitTests
{
    [TestFixture]
    public class PersonTest
    {
        Person person;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            person = new Person();
            person.fName = "Dan";
            person.mName = "Esteban";
            person.lName = "Cockburn";
            person.gender = "m";
            person.phone = "(519) 555-5555";
            person.email = "gramoszuniga5711@conestogac.on.ca";
            context = new ValidationContext(person, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Person), typeof(PersonMetadata)), typeof(Person));
        }

        [Test]
        public void NullFirstName()
        {
            person.fName = null;
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void EmptyFirstName()
        {
            person.fName = "";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void LargerFirstName()
        {
            person.fName = "ThisFirstNameIsWayMoreThanThirtyCharacters";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidFirstName()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullMiddleName()
        {
            person.regDate = DateTime.Today;
            person.mName = null;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void EmptyMiddleName()
        {
            person.regDate = DateTime.Today;
            person.mName = "";
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void LargerMiddleName()
        {
            person.mName = "ThisMiddleNameIsWayMoreThanThirtyCharacters";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidMiddleName()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullLastName()
        {
            person.lName = null;
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void EmptyLastName()
        {
            person.lName = "";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void LargerLastName()
        {
            person.lName = "ThisLastNameIsWayMoreThanThirtyCharacters";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidLastName()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullDOB()
        {
            Assert.IsNotNull(person.dob);
        }

        [Test]
        public void LaterDOB()
        {
            person.dob = new DateTime(2017, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidDOB()
        {
            person.regDate = DateTime.Today;
            person.dob = new DateTime(2016, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullGender()
        {
            person.gender = null;
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void EmptyGender()
        {
            person.gender = "";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void InvalidGender()
        {
            person.gender = "u";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidGender()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullPhone()
        {
            person.phone = null;
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void EmptyPhone()
        {
            person.phone = "";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void LargerPhone()
        {
            person.phone = "ThisPhoneIsWayMoreThanFourteenCharacters";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void InvalidPhone()
        {
            person.phone = "(519) 555";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidPhone()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullEmail()
        {
            person.email = null;
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void EmptyEmail()
        {
            person.email = "";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void LargerEmail()
        {
            person.email = "ThisLargeEmailIsWayMoreThanSixtyCharactersButINeedMoreLetterInThisString";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        public void InvalidEmail()
        {
            person.email = "gramoszuniga5711@conestogac";
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidEmail()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void NullRegistrationDate()
        {
            Assert.IsNotNull(person.regDate);
        }

        [Test]
        public void InvalidRegistrationDate()
        {
            person.regDate = new DateTime(2017, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(person, context, results, true));
        }

        [Test]
        public void ValidRegistrationDate()
        {
            person.regDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(person, context, results, true));
        }
    }
}