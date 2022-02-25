using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleHomePage : ContentPage
    {
        public BattleHomePage()
        {
            InitializeComponent();
        }


        public async void ScorePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScorePage()));
            _ = await Navigation.PopAsync();
        }

        public async void PickItemsPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PickItemsPage()));
            _ = await Navigation.PopAsync();
        }
    }
}