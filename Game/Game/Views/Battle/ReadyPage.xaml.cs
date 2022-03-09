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
    public partial class ReadyPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        // Empty Constructor for UTs
        bool UnitTestSetting;
        public ReadyPage(bool UnitTest) { UnitTestSetting = UnitTest; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ReadyPage()
        {
            InitializeComponent();
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.RoundCount = 1;
        }

        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BeginButton_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new NavigationPage(new NewRoundPage()));
            _ = await Navigation.PopAsync();
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