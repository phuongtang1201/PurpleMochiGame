using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Create Monster
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();
            this.ViewModel = data;

            this.ViewModel.Title = "Create";

            //This message will show if either the name entry box or description entry box is empty
            Warning_Not_Null_Message.Text = "Please enter a valid input.";
            Warning_Not_Null_Message.IsVisible = false;

            //Added item to ImagePicker
            GenerateImagePicker();

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Added all possible images for selection to ImagePicker
        /// </summary>
        private void GenerateImagePicker()
        {
            ImagePicker.Items.Add("eggs100px.png");
            ImagePicker.Items.Add("garlic100px.png");
            ImagePicker.Items.Add("lettuce100px.png");
            ImagePicker.Items.Add("steak100px.png");
            ImagePicker.Items.Add("pineapple.png");
            ImagePicker.Items.Add("strawberry100px.png");
            ImagePicker.Items.Add("watermelon100px.png");
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;

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
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            //Does not allow to save if either null Name or Description
            if (!Warning_Not_Null_Message.IsVisible)
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
        /// Catch the change to the Slider for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", Convert.ToInt32(e.NewValue));
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
        /// Catch the change to the Slider for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", Convert.ToInt32(e.NewValue));
        }

        ///// <summary>
        ///// 
        ///// Randomize the Monster
        ///// Keep the Level the Same
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public bool RandomizeMonster()
        //{
        //    // Randomize Name
        //    ViewModel.Data.Name = RandomPlayerHelper.GetMonsterName();
        //    ViewModel.Data.Description = RandomPlayerHelper.GetMonsterDescription();

        //    // Randomize the Attributes
        //    ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
        //    ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
        //    ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();

        //    ViewModel.Data.Difficulty = RandomPlayerHelper.GetMonsterDifficultyValue();

        //    ViewModel.Data.ImageURI = RandomPlayerHelper.GetMonsterImage();

        //    ViewModel.Data.UniqueItem = RandomPlayerHelper.GetMonsterUniqueItem();

        //    _ = UpdatePageBindingContext();

        //    return true;
        //}
    }
}