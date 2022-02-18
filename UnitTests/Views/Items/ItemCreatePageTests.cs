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
    public class ItemCreatePageTests : ItemCreatePage
    {
        App app;
        ItemCreatePage page;

        public ItemCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void ItemCreatePage_Save_Clicked_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = "icon_new.png";
            page.ViewModel.Data.Name = "Cookbook";
            page.ViewModel.Data.Description = "All the recipes";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnStepperValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ItemCreatePage_Value_OnSliderrValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ItemCreatePage_Range_OnSliderValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ItemCreatePage_Damage_OnSliderDamageChanged_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Entry_CheckNotEmpty_Null_Name_Should_Pass()
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
        public void ItemCreatePage_Entry_CheckNotEmpty_Null_Description_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Description = null;

            // Act
            page.Entry_CheckNotEmpty(null, null);


            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
        [Test]
        public void ItemCreatePage_Image_OnPickerValueChanged_Default_Should_Pass()
        {
            // Arrange
           
            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Image_OnPickerValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_RangeDamage_OnPickerValueChanged1to5_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemCreatePage();
            var oldEnum = 1;
            var newEnum = 5;

            var args = new ValueChangedEventArgs(oldEnum, newEnum);

            // Act
            page.RangeDamage_OnPickerValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemUpdatePage_RangeDamage_OnPickerValueChanged5to0_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemCreatePage();
            var oldEnum = 5;
            var newEnum = 0;

            var args = new ValueChangedEventArgs(oldEnum, newEnum);

            // Act
            page.RangeDamage_OnPickerValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }
    }
}