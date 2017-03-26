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
    class GameTest
    {
        Game game;
        ValidationContext context;
        List<ValidationResult> results;

        [SetUp]
        public void SetUp()
        {
            game = new Game();
            game.title = "The Legend of Zelda: Ocarina of Time";
            game.desc = "This is a great description";
            game.cover = "public\\images\\cover-1.mp3";
            game.publisher = "Nintendo";
            context = new ValidationContext(game, null, null);
            results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(Game), typeof(GameMetadata)), typeof(Game));
        }

        [Test]
        public void NullTitle()
        {
            game.title = null;
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void EmptyTitle()
        {
            game.title = "";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void LargerTitle()
        {
            game.title = "ThisTitleIsWayMoreThanSixtyCharactersButINeedMoreWordsInThisString";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void ValidTitle()
        {
            Assert.IsTrue(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void NullReleaseDate()
        {
            Assert.IsNotNull(game.relDate);
        }

        [Test]
        public void LaterReleaseDate()
        {
            game.relDate = new DateTime(2017, 10, 1);
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void ValidReleaseDate()
        {
            game.relDate = DateTime.Today;
            Assert.IsTrue(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void NullDescription()
        {
            game.desc = null;
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void EmptyDescription()
        {
            game.desc = "";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void LargerDescription()
        {
            game.desc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam ac vestibulum diam. Fusce porta efficitur nunc. Phasellus vestibulum, orci vel faucibus gravida, neque leo suscipit odio, ac volutpat sapien odio nec mauris. Aliquam pharetra massa quam, vitae vulputate nisl vulputate at. Morbi faucibus condimentum ultricies. Quisque id interdum sem. Cras luctus lacus ut placerat convallis. Duis elementum vel magna sit amet aliquet. Sed tempus elit justo, quis tristique leo sodales posuere. Nullam ultrices tristique est in ornare. Cras quis sapien dapibus nunc commodo condimentum id vitae mauris. Proin lacinia, nunc a laoreet gravida, ante nisi egestas lectus, ut commodo justo dolor non massa. Proin molestie elit eget interdum aliquam. Donec rutrum pharetra tempor.Nunc faucibus quam sit amet est mollis lobortis.Aliquam mollis eros ipsum, in hendrerit tortor consectetur et.Praesent sit amet nulla sit amet dolor suscipit malesuada sodales pulvinar erat.Morbi ut ultricies arcu, et pharetra mi.Maecenas a euismod tellus, pulvinar laoreet urna.Nam sed arcu purus. Aenean fringilla varius erat sagittis maximus.Nunc rutrum accumsan tristique.Donec aliquam id sapien quis malesuada.Morbi commodo ipsum lacus, molestie aliquet nisl sollicitudin a.Suspendisse tristique et erat ut molestie.Integer elit metus, facilisis dignissim ante id, rutrum semper tortor.Nunc interdum ex eget rhoncus rutrum.Aenean eu imperdiet libero. Ut commodo nunc vitae interdum convallis.Nulla quam dui, interdum a ligula ac, varius accumsan felis.Cras suscipit massa et mauris pretium, ac rutrum urna dignissim.Vivamus non nunc non ex fringilla ultricies.Donec lacinia in neque eget faucibus.Fusce nulla elit, porta ac lectus";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void ValidDescription()
        {
            Assert.IsTrue(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void NullCover()
        {
            game.cover = null;
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void EmptyCover()
        {
            game.cover = "";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void LargerCover()
        {
            game.cover = "ThisCoverExampleIsWayMoreThanOneHundredCharactersButIReallyNeedManyMoreWordsInThisExampleStringThisTime";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void ValidCover()
        {
            Assert.IsTrue(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void NullPublisher()
        {
            game.publisher = null;
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void EmptyPublisher()
        {
            game.publisher = "";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void LargerPublisher()
        {
            game.publisher = "ThisPublisherExampleIsMoreThanThirdtyCharactersButINeedMoreWordsInThisString";
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void ValidPublisher()
        {
            Assert.IsTrue(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void NullAverageRating()
        {
            Assert.IsNull(game.rateAVG);
        }

        [Test]
        public void NumericAverageRating()
        {
            game.rateAVG = 5.00m;
            Assert.IsInstanceOf(typeof(decimal), game.rateAVG);
        }

        [Test]
        public void LargerAverageRating()
        {
            game.rateAVG = 5.01m;
            Assert.IsFalse(Validator.TryValidateObject(game, context, results, true));
        }

        [Test]
        public void ValidAverageRating()
        {
            game.rateAVG = 5.00m;
            Assert.IsTrue(Validator.TryValidateObject(game, context, results, true));
        }
    }
}