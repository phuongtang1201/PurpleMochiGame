using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.GameRules;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Create Character
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class CharacterCreatePage : ContentPage
    {
        // The Character to create
        public GenericViewModel<CharacterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;


        // Empty Constructor for UTs
        public CharacterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharacterCreatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            data.Data = new CharacterModel();
            this.ViewModel = data;

            this.ViewModel.Title = "Create Chef";

            // Load the values for the Level into the Picker
            for (var i = 1; i <= LevelTableHelper.MaxLevel; i++)
            {
                LevelPicker.Items.Add(i.ToString());
            }

            //This message will show if either the name entry box or description entry box is empty
            Warning_Not_Null_Message.Text = "Please enter a valid input.";
            Warning_Not_Null_Message.IsVisible = false;


            //This message will show if either image, location or attribute box is not selected
            Warning_Select_Message.Text = "Please select an item.";
            Warning_Select_Message.IsVisible = false;

            this.ViewModel.Data.Level = 1;
            // LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            _ = UpdatePageBindingContext();

        }

        /// <summary>
        /// Added all possible images for selection to ImagePicker
        /// </summary>
        private void GenerateImagePicker()
        {
            ImagePicker.Items.Clear();

            ImagePicker.Items.Add("chefblue.png");
            ImagePicker.Items.Add("chefgreen.png");
            ImagePicker.Items.Add("cheforange.png");
            ImagePicker.Items.Add("chefpurple.png");
            ImagePicker.Items.Add("chefwhite.png");
            ImagePicker.Items.Add("chefyellow.png");

            //Binding image picker with the current value of ImageURI
            ImagePicker.SelectedItem = ViewModel.Data.ImageURI.ToString();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var level = this.ViewModel.Data.Level;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            // This resets the Picker to -1 index, need to reset it back
            ViewModel.Data.Level = level;
            LevelPicker.SelectedIndex = ViewModel.Data.Level - 1;

            ManageHealth();

            AddItemsToDisplay();

            // Added item to ImagePicker
            GenerateImagePicker();

            return true;
        }

        /// <summary>
        /// Check the entry box to guarantee entering a not null value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Entry_CheckNotEmpty(object sender, ValueChangedEventArgs e)
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
            }
        }

        /// <summary>
        /// The Level selected from the list
        /// Need to recalculate Max Health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Level_Changed(object sender, EventArgs args)
        {
            // Change the Level
            ViewModel.Data.Level = LevelPicker.SelectedIndex + 1;

            ManageHealth();
        }

        /// <summary>
        /// Change the Level Picker
        /// </summary>
        public void ManageHealth()
        {
            // Roll for new HP
            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            // Show the Result
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new CharacterModel().ImageURI;
            }
            //Check if image is selected
            CheckPickerNotNull();

            //Does not allow to save if either null Name or Description or image
            if (!Warning_Not_Null_Message.IsVisible && !Warning_Select_Message.IsVisible)
            {
                MessagingCenter.Send(this, "Create", ViewModel.Data);
                _ = await Navigation.PopModalAsync();
            }
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Check all pickers: Image is not null
        /// </summary>
        public void CheckPickerNotNull()
        {
            Warning_Select_Message.IsVisible = false;

            if (ImagePicker.SelectedIndex == -1)
            {
                Warning_Select_Message.IsVisible = true;
            }
        }

        /// <summary>
        /// Save the selected item to ImageURI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Image_OnPickerValueChanged(object sender, EventArgs e)
        {
            if (ImagePicker.SelectedItem is null)
            {
                return;
            }

            ViewModel.Data.ImageURI = ImagePicker.SelectedItem.ToString();
            LargeImage.Source = ViewModel.Data.ImageURI;
        }


        /// <summary>
        /// Catch the change to the Stepper for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the Slider for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the Stepper for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the Slider for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the Stepper for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", e.NewValue);
        }

        /// <summary>
        /// Catch the change to the Slider for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            _ = ViewModel.Data.AddItem(PopupLocationEnum, data.Id);

            AddItemsToDisplay();

            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Equipment for:";
            PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location));

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemBox.Children.Remove(data);
            }

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "icon_add.png";

            var data = ViewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                data = new ItemModel { Location = location, ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(location);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RollDice_Clicked(object sender, EventArgs e)
        {
            _ = DiceAnimationHandeler();

            _ = RandomizeCharacter();

            return;
        }

        /// <summary>
        /// 
        /// Randomize the Character
        /// Keep the Level the Same
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RandomizeCharacter()
        {
            // Randomize Name
            ViewModel.Data.Name = RandomPlayerHelper.GetCharacterName();
            ViewModel.Data.Description = RandomPlayerHelper.GetCharacterDescription();

            // Randomize the Attributes
            ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();

            // Randomize an Item for Location
            ViewModel.Data.Head = RandomPlayerHelper.GetItem(ItemLocationEnum.Head);
            ViewModel.Data.Necklass = RandomPlayerHelper.GetItem(ItemLocationEnum.Necklass);
            ViewModel.Data.PrimaryHand = RandomPlayerHelper.GetItem(ItemLocationEnum.PrimaryHand);
            ViewModel.Data.OffHand = RandomPlayerHelper.GetItem(ItemLocationEnum.OffHand);
            ViewModel.Data.RightFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.LeftFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.Feet = RandomPlayerHelper.GetItem(ItemLocationEnum.Feet);

            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            ViewModel.Data.ImageURI = RandomPlayerHelper.GetCharacterImage();

            _ = UpdatePageBindingContext();

            return true;
        }

        /// <summary>
        /// Setup the Dice Animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool DiceAnimationHandeler()
        {
            // Animate the Rolling of the Dice
            var image = RollDice;
            uint duration = 1000;

            var parentAnimation = new Animation();

            // Grow the image Size
            var scaleUpAnimation = new Animation(v => image.Scale = v, 1, 2, Easing.SpringIn);

            // Spin the Image
            var rotateAnimation = new Animation(v => image.Rotation = v, 0, 360);

            // Shrink the Image
            var scaleDownAnimation = new Animation(v => image.Scale = v, 2, 1, Easing.SpringOut);

            parentAnimation.Add(0, 0.5, scaleUpAnimation);
            parentAnimation.Add(0, 1, rotateAnimation);
            parentAnimation.Add(0.5, 1, scaleDownAnimation);

            parentAnimation.Commit(this, "ChildAnimations", 16, duration, null, null);

            return true;
        }
    }
}