using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRoundPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public NewRoundPage()
        {
            InitializeComponent();

            // Update the Round Count
            //TotalRound.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount.ToString();

            // Draw the Characters
            foreach (var data in EngineViewModel.Engine.EngineSettings.CharacterList)
            {
                PartyListFrame.Children.Add(CreatePlayerDisplayBox(data));
            }

        }

        /// <summary>
        /// Go back to Pick Items screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void AddItems_Clicked(object sender, EventArgs e)
        {
            //_ = await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new NavigationPage(new PickItemsPage()));
        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BeginButton_Clicked(object sender, EventArgs e)
        {
            //_ = await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
        }

        /// <summary>
        /// Change settings, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            //_ = await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new NavigationPage(new BattleSettingsPage()));
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleExtraLargeStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 30,
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }
    }
}