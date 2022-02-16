using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreReadPage : ContentPage
    {
        // View Model for Score
        public readonly GenericViewModel<ScoreModel> ViewModel;

        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        // Empty Constructor for UTs
        public ScoreReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public ScoreReadPage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();
            DrawOutput();

            BindingContext = this.ViewModel = data;
        }

        /// <summary>
        /// Draw data for
        /// Character
        /// Monster
        /// Item
        /// </summary>
        public void DrawOutput()
        {

            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.EngineSettings.BattleScore.CharacterModelDeathList)
            {
                CharacterListFrame.Children.Add(CreateCharacterDisplayBox(data));
            }

            // Count duplicate Monsters
            var Monsters = from monsters in EngineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList
                           group monsters by monsters.ImageURI into duplicates
                           orderby duplicates.Key
                           let count = duplicates.Count()
                           select new { Value = duplicates.First(), Count = count };

            // Draw the Monsters
            for(var index = 0; index < Monsters.Count(); index++)
            {
                var data = Monsters.ElementAt(index).Value;
                var count = Monsters.ElementAt(index).Count;
                MonsterListFrame.Children.Add(CreateMonsterDisplayBox(data, count));
            }

            // Count duplicate Items
            var Items = from items in EngineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList
                           group items by items.ImageURI into duplicates
                           orderby duplicates.Key
                           let count = duplicates.Count()
                           select new { Value = duplicates.First(), Count = count };

            // Draw the Items
            for (var index = 0; index < Monsters.Count(); index++)
            {
                var data = Items.ElementAt(index).Value;
                var count = Items.ElementAt(index).Count;
                ItemListFrame.Children.Add(CreateItemDisplayBox(data, count));
            }

            // Update Values in the UI
            //TotalKilled.Text = EngineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Count().ToString();
            //TotalCollected.Text = EngineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();
            //TotalScore.Text = EngineViewModel.Engine.EngineSettings.BattleScore.ExperienceGainedTotal.ToString();
        }

        /// <summary>
        /// Return a stack layout for the Characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateCharacterDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreCharacterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Return a stack layout for the Monsters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateMonsterDisplayBox(PlayerInfoModel data, int count)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Add the number of duplicates for this monster
            var PlayerDuplicatesLabel = new Label
            {
                Text = " x " + count,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreMonsterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerDuplicatesLabel,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateItemDisplayBox(ItemModel data, int count)
        {
            if (data == null)
            {
                data = new ItemModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Add the number of duplicates for this item
            var PlayerDuplicatesLabel = new Label
            {
                Text = " x " + count,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreItemInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerDuplicatesLabel,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScoreUpdatePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScoreDeletePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }
    }
}