using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ScoreUpdatePageTests : ScoreUpdatePage
    {
        App app;
        ScoreUpdatePage page;

        public ScoreUpdatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ScoreUpdatePage(new GenericViewModel<ScoreModel>(new ScoreModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ScoreUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScoreUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_Entry_CheckNotEmpty_Null_Name_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = null;

            // Act
            page.Entry_CheckNotEmpty(null, null);


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_CreateCharacterDisplayBox_Null_Player_Should_Pass()
        {
            // Arrange

            // Act
            page.CreateCharacterDisplayBox(null);


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_CreateItemDisplayBox_Null_Item_Should_Pass()
        {
            // Arrange

            // Act
            page.CreateItemDisplayBox(null, 0);


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreUpdatePage_CreateMonsterDisplayBox_Null_Monster_Should_Pass()
        {
            // Arrange

            // Act
            page.CreateMonsterDisplayBox(null, 0);


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}