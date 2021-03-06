using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Game.Views
{
    public class SelectCharacterModel : CharacterModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }

        public Style SelectedStyleTrue = (Style)Application.Current.Resources["BattleSelectedCharacterFrameTrue"];
        public Style SelectedStyleFalse = (Style)Application.Current.Resources["BattleSelectedCharacterFrameFalse"];

        public Style FrameStyle
        {
            get
            {
                if (isSelected)
                {
                    return SelectedStyleTrue;
                }

                return SelectedStyleFalse;
            }
        }
    }

    /// <summary>
    /// Selecting Characters for the Game 
    /// 
    /// Up to 6 characters can be chosen with no duplicates
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {
        private int preIndex;

        public ObservableCollection<SelectCharacterModel> PossibleCharacters { get; set; } = new ObservableCollection<SelectCharacterModel>();

        // Empty Constructor for UTs
        public PickCharactersPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = BattleEngineViewModel.Instance;

            // Clear the Database List and the Party List to start
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();

            UpdateNextButtonState();

            // Populate a list of possible characters from the existing characters
            foreach (var item in BattleEngineViewModel.Instance.DatabaseCharacterList)
            {
                PossibleCharacters.Add(new SelectCharacterModel() { IsSelected = false, ImageURI = item.ImageURI, 
                    Name = item.Name, Description = item.Description, Level = item.Level, MaxHealth = item.MaxHealth, 
                    Speed = item.Speed, Defense = item.Defense, Attack = item.Attack, Id=item.Id });
            }

            CharacterSourceList.ItemsSource = PossibleCharacters;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnDatabaseCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as SelectCharacterModel;
            if (data == null)
            {
                return;
            }

            if (preIndex == args.SelectedItemIndex)
            {
                CharacterSourceList.SelectedItem = null;
                preIndex = -1;
            }
            else
            {
                preIndex = args.SelectedItemIndex;
            }

            if (data.IsSelected)
            {
                data.IsSelected = false;

                // Remove the character from the list
                var character = BattleEngineViewModel.Instance.DatabaseCharacterList.FirstOrDefault(m => m.Id.Equals(data.Id));
                if (character != null)
                {
                    BattleEngineViewModel.Instance.PartyCharacterList.Remove(character);
                }

            }
            else
            {
                data.IsSelected = true;

                // Don't add more than the party max
                if (BattleEngineViewModel.Instance.PartyCharacterList.Count() < BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters)
                {
                    var characterExist = BattleEngineViewModel.Instance.PartyCharacterList.Any(m => m.Id.Equals(data.Id));
                    if (characterExist == false)
                    {
                        // Not in the list, so add
                        var character = BattleEngineViewModel.Instance.DatabaseCharacterList.FirstOrDefault(m => m.Id.Equals(data.Id));
                        if (character != null)
                        {
                            BattleEngineViewModel.Instance.PartyCharacterList.Add(character);
                        }
                    }
                }
            }

            UpdateNextButtonState();
        }

        /// <summary>
        /// Next Button is based on the count
        /// 
        /// If no selected characters, disable
        /// 
        /// Show the Count of the party
        /// 
        /// </summary>
        public void UpdateNextButtonState()
        {
            // If no characters disable Next button
            BeginBattleButton.IsEnabled = true;

            var currentCount = BattleEngineViewModel.Instance.PartyCharacterList.Count();
            if (currentCount == 0)
            {
                BeginBattleButton.IsEnabled = false;
            }

        }

        /// <summary>
        /// Jump to the Ready page
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {
            CreateEngineCharacterList();

            //await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            await Navigation.PushModalAsync(new NavigationPage(new PickItemsPage()));
        }
        

        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        public void CreateEngineCharacterList()
        {
            // Clear the current list
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in BattleEngineViewModel.Instance.PartyCharacterList)
            {
                data.CurrentHealth = data.GetMaxHealthTotal;
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            }
        }
    }
}