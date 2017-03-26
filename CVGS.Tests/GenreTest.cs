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
    class GenreTest
    {
        Genre genre;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            genre = new Genre();
            genre.genreCode = "Horror";
            context = new ValidationContext(genre, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Genre), typeof(GenreMetadata)), typeof(Genre));
        }

        [Test]
        public void NullGenreCode()
        {
            genre.genreCode = null;
            Assert.IsFalse(Validator.TryValidateObject(genre, context, results, true));
        }

        [Test]
        public void EmptyGenreCode()
        {
            genre.genreCode = "";
            Assert.IsFalse(Validator.TryValidateObject(genre, context, results, true));
        }

        [Test]
        public void LargerGenreCode()
        {
            genre.genreCode = "ThisGenreCodeIsWayMoreThanThirtyCharacters";
            Assert.IsFalse(Validator.TryValidateObject(genre, context, results, true));
        }

        [Test]
        public void ValidGenreCode()
        {
            Assert.IsTrue(Validator.TryValidateObject(genre, context, results, true));
        }
    }
}