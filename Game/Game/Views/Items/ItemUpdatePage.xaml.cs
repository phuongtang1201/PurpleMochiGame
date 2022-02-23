using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Hold a copy of the original data for Cancel to use
        public ItemModel DataCopy;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            DataCopy = new ItemModel(data.Data);

            this.ViewModel.Title = "Update " + data.Title;

            //This message will show if either the name entry box or description entry box is empty
            Warning_Not_Null_Message.Text = "Please enter a valid input.";
            Warning_Not_Null_Message.IsVisible = false;

            //This message will show if either the image, location or attribute is not selected
            Warning_Select_Message.Text = "Please select an item.";
            Warning_Select_Message.IsVisible = false;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();

            //This will make the range and damage invisible until primary hand is selected
            RangeDamageGrid.IsVisible = false;

            //Added item to ImagePicker
            GenerateImagePicker();

            //Binding image picker with the current value of ImageURI
            ImagePicker.SelectedItem = ViewModel.Data.ImageURI;
        }

        /// <summary>
        /// Added all possible images for selection to ImagePicker
        /// </summary>
        private void GenerateImagePicker()
        {
            ImagePicker.Items.Add("apron.png");
            ImagePicker.Items.Add("coffee.png");
            ImagePicker.Items.Add("cookbook.png");
            ImagePicker.Items.Add("knife.png");
            ImagePicker.Items.Add("nonslip.png");
            ImagePicker.Items.Add("rollingpin.png");
            ImagePicker.Items.Add("salt.png");
            ImagePicker.Items.Add("soap.png");
            ImagePicker.Items.Add("spatula.png");
            ImagePicker.Items.Add("sponge.png");
            ImagePicker.Items.Add("woodenspoon.png");
            ImagePicker.Items.Add("vinegar.png");
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            //chek all pickers not null
            CheckPickerNotNull();

            // Check the name and description and all pickers is not null
            if (!Warning_Not_Null_Message.IsVisible && !Warning_Select_Message.IsVisible)
            {
                MessagingCenter.Send(this, "Update", ViewModel.Data);
                _ = await Navigation.PopModalAsync();
            }
           
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Put copy back
            ViewModel.Data.Update(DataCopy);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Check all pickers: Image, Location, Attribute is not null
        /// </summary>
        public void CheckPickerNotNull()
        {
            Warning_Select_Message.IsVisible = false;
            if (!string.IsNullOrEmpty(LocationPicker.SelectedItem.ToString()) && LocationPicker.SelectedItem.ToString() == "Unknown")
            {
                Warning_Select_Message.IsVisible = true;
            }
            if (!string.IsNullOrEmpty(AttributePicker.SelectedItem.ToString()) && AttributePicker.SelectedItem.ToString() == "Unknown")
            {
                Warning_Select_Message.IsVisible = true;
            }
            if (ImagePicker.SelectedIndex == -1)
            {
                Warning_Select_Message.IsVisible = true;
            }

        }

        /// <summary>
        /// Check the entry box to guarantee entering a not null value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Entry_CheckNotEmpty (object sender, ValueChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                Warning_Not_Null_Message.IsVisible = true;
                return;
            }

            if (!string.IsNullOrEmpty(ViewModel.Data.Name))
            {
                Warning_Not_Null_Message.IsVisible = false;
            }

            if (string.IsNullOrEmpty(ViewModel.Data.Description))
            {
                Warning_Not_Null_Message.IsVisible = true;
                return;
            }

            if (!string.IsNullOrEmpty(ViewModel.Data.Description))
            {
                Warning_Not_Null_Message.IsVisible = false;
                return;
            }

        }

        /// <summary>
        /// Catch the change to the Slider for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = string.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the slider for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = string.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the slider for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = string.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// Remove Range and Damage Steppers if Location isn't PrimaryHand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RangeDamage_OnPickerValueChanged(object sender, EventArgs e)
        {
            RangeDamageGrid.IsVisible = false;

            if (LocationPicker.SelectedIndex == 5)
            {
                RangeDamageGrid.IsVisible = true;
            }  
        }

        /// <summary>
        /// Save the selected item to ImageURI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Image_OnPickerValueChanged(object sender, EventArgs e)
        {
            ViewModel.Data.ImageURI = ImagePicker.SelectedItem.ToString();
            LargeImage.Source = ViewModel.Data.ImageURI;
        }

    }
}