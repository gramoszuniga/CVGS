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
    class ReviewTest
    {
        Review review;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            review = new Review();
            review.status = "Pending";
            review.revComment = "This is a great comment";
            context = new ValidationContext(review, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Review), typeof(ReviewMetadata)), typeof(Review));
        }

        [Test]
        public void NullRating()
        {
            Assert.IsNotNull(review.rating);
        }

        [Test]
        public void NumericRating()
        {
            Assert.IsInstanceOf(typeof(int), review.rating);
        }

        [Test]
        public void LargerRating()
        {
            review.rating = -1;
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void ValidRating()
        {
            review.revDate = DateTime.Today;
            review.rating = 5;
            Assert.IsTrue(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void NullStatus()
        {
            review.status = null;
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void EmptyStatus()
        {
            review.status = "";
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void InvalidStatus()
        {
            review.status = "Processed";
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void ValidStatus()
        {
            review.revDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void NullReviewDate()
        {
            Assert.IsNotNull(review.revDate);
        }

        [Test]
        public void InvalidReviewDate()
        {
            review.revDate = new DateTime(2015, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void ValidReviewDate()
        {
            review.revDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void NullReviewComment()
        {
            review.revComment = null;
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void EmptyReviewComment()
        {
            review.revComment = "";
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void LargerReviewComment()
        {
            review.revComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ac vestibulum diam. Fusce porta efficitur nunc. Phasellus vestibulum, orci vel faucibus gravida, neque leo suscipit odio, ac volutpat sapien odio nec mauris. Aliquam pharetra massa quam, vitae vulputate nisl vulputate at. Morbi faucibus condimentum ultricies. Quisque id interdum sem. Cras luctus lacus ut placerat convallis. Duis elementum vel magna sit amet aliquet. Sed tempus elit justo, quis tristique leo sodales posuere. Nullam ultrices tristique est in ornare. Cras quis sapien dapibus nunc commodo condimentum id vitae mauris. Proin lacinia, nunc a laoreet gravida, ante nisi egestas lectus, ut commodo justo dolor non massa. Proin molestie elit eget interdum aliquam. Donec rutrum pharetra tempor.Nunc faucibus quam sit amet est mollis lobortis.Aliquam mollis eros ipsum, in hendrerit tortor consectetur et.Praesent sit amet nulla sit amet dolor suscipit malesuada sodales pulvinar erat.Morbi ut ultricies arcu, et pharetra mi.Maecenas a euismod tellus, pulvinar laoreet urna.Nam sed arcu purus. Aenean fringilla varius erat sagittis maximus.Nunc rutrum accumsan tristique.Donec aliquam id sapien quis malesuada.Morbi commodo ipsum lacus, molestie aliquet nisl sollicitudin a.Suspendisse tristique et erat ut molestie.Integer elit metus, facilisis dignissim ante id, rutrum semper tortor.Nunc interdum ex eget rhoncus rutrum.Aenean eu imperdiet libero. Ut commodo nunc vitae interdum convallis.Nulla quam dui, interdum a ligula ac, varius accumsan felis.Cras suscipit massa et mauris pretium, ac rutrum urna dignissim.Vivamus non nunc non ex fringilla ultricies.Donec lacinia in neque eget faucibus.Fusce nulla elit, porta ac lectus";
            Assert.IsFalse(Validator.TryValidateObject(review, context, results, true));
        }

        [Test]
        public void ValidReviewComment()
        {
            review.revDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(review, context, results, true));
        }
    }
}