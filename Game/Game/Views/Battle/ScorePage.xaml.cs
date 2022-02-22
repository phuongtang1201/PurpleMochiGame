using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScorePage()
        {
            InitializeComponent();
            DrawOutput();
        }

        /// <summary>
        /// Draw data for
        /// Character
        /// Monster
        /// Item
        /// </summary>
        public void DrawOutput()
        {
            // Count duplicate Monsters
            var Monsters = from monsters in EngineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList
                           group monsters by monsters.ImageURI into duplicates
                           orderby duplicates.Key
                           let count = duplicates.Count()
                           select new { Value = duplicates.First(), Count = count };

            // Draw the Monsters
            for (var index = 0; index < Monsters.Count(); index++)
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
            TotalScore.Text = EngineViewModel.Engine.EngineSettings.BattleScore.ExperienceGainedTotal.ToString();
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

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
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
                Style = (Style)Application.Current.Resources["ScoreCharacterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerLevelLabel,
                },
            };

            return PlayerStack;
        }

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
        /// Close the Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }
    }
}