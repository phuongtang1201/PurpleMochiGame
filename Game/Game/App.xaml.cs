using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Views;

[assembly: ExportFont("futura medium condensed bt.ttf", Alias = "Futura")]

namespace Game
{
    /// <summary>
    /// Main Application entry point
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        /// <summary>
        /// Default App Constructor
        /// </summary>
        [System.Obsolete]
        public App()
        {
            InitializeComponent();

            // Add each model here to warm up and load it.
            _ = Helpers.DataSetsHelper.WarmUp();

            // Call the Main Page to open
            MainPage = new MainPage();
        }

        /// <summary>
        /// On Startup code if needed
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// On Sleep code if needed
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// On App Resume code if needed
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}