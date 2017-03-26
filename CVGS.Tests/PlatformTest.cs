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
    class PlatformTest
    {
        Platform platform;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            platform = new Platform();
            platform.platformCode = "N64";
            context = new ValidationContext(platform, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Platform), typeof(PlatformMetadata)), typeof(Platform));
        }

        [Test]
        public void NullPlatformCode()
        {
            platform.platformCode = null;
            Assert.IsFalse(Validator.TryValidateObject(platform, context, results, true));
        }

        [Test]
        public void EmptyPlatformCode()
        {
            platform.platformCode = "";
            Assert.IsFalse(Validator.TryValidateObject(platform, context, results, true));
        }

        [Test]
        public void LargerPlatformCode()
        {
            platform.platformCode = "ThisPlatformCodeIsWayMoreThanThirtyCharacters";
            Assert.IsFalse(Validator.TryValidateObject(platform, context, results, true));
        }

        [Test]
        public void ValidPlatformCode()
        {
            Assert.IsTrue(Validator.TryValidateObject(platform, context, results, true));
        }
    }
}