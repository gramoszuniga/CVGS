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
    class EventTest
    {
        Event evnt;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            evnt = new Event();
            evnt.title = "Lan Party";
            evnt.venue = "Conestoga College The Venue";
            evnt.notes = "These are great notes";
            context = new ValidationContext(evnt, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Event), typeof(EventMetadata)), typeof(Event));
        }

        [Test]
        public void NullStartDate()
        {
            Assert.IsNotNull(evnt.startDate);
        }

        [Test]
        public void EarlierStartDate()
        {
            evnt.startDate = new DateTime(2015, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void ValidStartDate()
        {
            evnt.startDate = new DateTime(2017, 10, 1);
            evnt.endDate = new DateTime(2018, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void NullEndDate()
        {
            Assert.IsNotNull(evnt.endDate);
        }

        [Test]
        public void EarlierEndDateThanStartDate()
        {
            evnt.startDate = new DateTime(2017, 10, 1);
            evnt.endDate = new DateTime(2015, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void ValidEndDate()
        {
            evnt.startDate = new DateTime(2017, 10, 1);
            evnt.endDate = new DateTime(2018, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void NullRegistrationFee()
        {
            Assert.IsNotNull(evnt.regFee);
        }

        [Test]
        public void NumericRegistrationFee()
        {
            Assert.IsInstanceOf(typeof(decimal), evnt.regFee);
        }

        [Test]
        public void LargerRegistrationFee()
        {
            evnt.regFee = 1000.00m;
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void ValidRegistrationFee()
        {
            evnt.regFee = 9.99m;
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void NullVenue()
        {
            evnt.venue = null;
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void EmptyVenue()
        {

        }

        [Test]
        public void LargerVenue()
        {
            evnt.venue = "ThisVenueExampleIsWayMoreThanOneHundredCharactersButIReallyNeedManyMoreWordsInThisExampleStringThisTime";
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void ValidVenue()
        {
            evnt.startDate = new DateTime(2017, 10, 1);
            evnt.endDate = new DateTime(2018, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void NullNotes()
        {
            evnt.notes = null;
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void EmptyNotes()
        {
            evnt.notes = "";
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void LargerNotes()
        {
            evnt.notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ac vestibulum diam. Fusce porta efficitur nunc. Phasellus vestibulum, orci vel faucibus gravida, neque leo suscipit odio, ac volutpat sapien odio nec mauris. Aliquam pharetra massa quam, vitae vulputate nisl vulputate at. Morbi faucibus condimentum ultricies. Quisque id interdum sem. Cras luctus lacus ut placerat convallis. Duis elementum vel magna sit amet aliquet. Sed tempus elit justo, quis tristique leo sodales posuere. Nullam ultrices tristique est in ornare. Cras quis sapien dapibus nunc commodo condimentum id vitae mauris. Proin lacinia, nunc a laoreet gravida, ante nisi egestas lectus, ut commodo justo dolor non massa. Proin molestie elit eget interdum aliquam. Donec rutrum pharetra tempor.Nunc faucibus quam sit amet est mollis lobortis.Aliquam mollis eros ipsum, in hendrerit tortor consectetur et.Praesent sit amet nulla sit amet dolor suscipit malesuada sodales pulvinar erat.Morbi ut ultricies arcu, et pharetra mi.Maecenas a euismod tellus, pulvinar laoreet urna.Nam sed arcu purus. Aenean fringilla varius erat sagittis maximus.Nunc rutrum accumsan tristique.Donec aliquam id sapien quis malesuada.Morbi commodo ipsum lacus, molestie aliquet nisl sollicitudin a.Suspendisse tristique et erat ut molestie.Integer elit metus, facilisis dignissim ante id, rutrum semper tortor.Nunc interdum ex eget rhoncus rutrum.Aenean eu imperdiet libero. Ut commodo nunc vitae interdum convallis.Nulla quam dui, interdum a ligula ac, varius accumsan felis.Cras suscipit massa et mauris pretium, ac rutrum urna dignissim.Vivamus non nunc non ex fringilla ultricies.Donec lacinia in neque eget faucibus.Fusce nulla elit, porta ac lectus";
            Assert.IsFalse(Validator.TryValidateObject(evnt, context, results, true));
        }

        [Test]
        public void ValidNotes()
        {
            evnt.startDate = new DateTime(2017, 10, 1);
            evnt.endDate = new DateTime(2018, 10, 1);
            Assert.IsTrue(Validator.TryValidateObject(evnt, context, results, true));
        }
    }
}