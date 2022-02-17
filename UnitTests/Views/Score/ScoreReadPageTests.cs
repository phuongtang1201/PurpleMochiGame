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
    public class ScoreReadPageTests : ScoreReadPage
    {
        App app;
        ScoreReadPage page;

        public ScoreReadPageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initialize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ScoreReadPage(new GenericViewModel<ScoreModel>(new ScoreModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ScoreReadPage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScoreReadPage_Update_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Update_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreReadPage_Delete_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Delete_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreReadPage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreReadPage_Create_Character_Display_Box_Data_Null_Should_Pass()
        {
            // Arrange
            PlayerInfoModel data = null;
            // Act
            StackLayout newStack = CreateCharacterDisplayBox(data);

            // Reset

            // Assert
            Assert.IsNotNull(newStack);
        }

        [Test]
        public void ScoreReadPage_Create_Item_Display_Box_Data_Null_Should_Pass()
        {
            // Arrange
            ItemModel data = null;
            // Act
            StackLayout newStack = CreateItemDisplayBox(data, 1);

            // Reset

            // Assert
            Assert.IsNotNull(newStack);
        }

        [Test]
        public void ScoreReadPage_Create_Item_Display_Box_Four_Items_Should_Pass()
        {
            // Arrange
            ItemModel data = null;
            // Act
            StackLayout newStack = CreateItemDisplayBox(data, 4);
            var stackChildren = newStack.Children;
            var methodLabel = new Label();
            methodLabel = (Label)stackChildren[1];

            var testLabel = new Label
            {
                Text = " x 4",
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            Assert.AreEqual(methodLabel.Text, testLabel.Text);

            // Reset

            // Assert
            Assert.IsNotNull(newStack);
        }
    }
}