using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        // Hold the Map Objects, for easy access to update them
        public Dictionary<string, object> MapLocationObject = new Dictionary<string, object>();


        // Empty Constructor for UTs
        bool UnitTestSetting;
        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Battling
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Set up the UI to Defaults
            BindingContext = BattleEngineViewModel.Instance;

            // Create and Draw the Map
            _ = InitializeMapGrid();

            // Start the Battle Engine
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            // Populate the UI Map
            DrawMapGridInitialState();

            // Ask the Game engine to select who goes first
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Add Players to Display
            DrawGameAttackerDefenderBoard();

            // Set the Battle Mode
            ShowBattleMode();
        }

        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                _ = CharacterBox.Children.Remove(data);
            }

            //// Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                _ = MonsterBox.Children.Remove(data);
              
            }

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(MonsterInfoDisplayBox(data));
                
            }

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Monster into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout MonsterInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new ImageButton
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumImageButtonStyle"],
                Source = data.ImageURI

            };

            //When click the button, select this monster as target
        //    PlayerImage.Clicked += (sender, args) => SelectMonster_Clicked(data);

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Select this monster as target
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public bool SelectMonster_Clicked(PlayerInfoModel data)
        //{
        //    if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
        //    {
        //        return false;
        //    }

        //    if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
        //    {
        //        BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender = data;
        //    }
        //    DrawGameBoardAttackerDefenderSection();
        //    return true;
        //}

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                Source = data.ImageURI
                
            };
            

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                Children = {
                    PlayerImage,
                },
            };
            

            return PlayerStack;
        }

        #region BattleMapMode

        /// <summary>
        /// Create the Initial Map Grid
        /// 
        /// All lcoations are empty
        /// </summary>
        /// <returns></returns>
        public bool InitializeMapGrid()
        {
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

            return true;
        }

        /// <summary>
        /// Draw the Map Grid
        /// Add the Players to the Map
        /// 
        /// Need to have Players in the Engine first, to then put on the Map
        /// 
        /// The Map Objects are all created with the map background image first
        /// 
        /// Then the actual characters are added to the map
        /// </summary>
        public void DrawMapGridInitialState()
        {
            // Create the Map in the Game Engine
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            _ = CreateMapGridObjects();

            _ = UpdateMapGrid();
        }

        /// <summary>
        /// Walk the current grid
        /// check each cell to see if it matches the engine map
        /// Update only those that need change
        /// </summary>
        /// <returns></returns>
        public bool UpdateMapGrid()
        {
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                // Use the ImageButton from the dictionary because that represents the player object
                var MapObject = GetMapGridObject(GetDictionaryImageButtonName(data));
                if (MapObject == null)
                {
                    return false;
                }

                var imageObject = (ImageButton)MapObject;

                // Check automation ID on the Image, That should match the Player, if not a match, the cell is now different need to update
                if (!imageObject.AutomationId.Equals(data.Player.Guid))
                {
                    // The Image is different, so need to re-create the Image Object and add it to the Stack
                    // That way the correct monster is in the box.

                    MapObject = GetMapGridObject(GetDictionaryStackName(data));
                    if (MapObject == null)
                    {
                        return false;
                    }

                    var stackObject = (StackLayout)MapObject;

                    // Remove the ImageButton
                    stackObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    stackObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    _ = MapGridObjectAddImage(PlayerImageButton, data);

                    stackObject.BackgroundColor = DetermineMapBackgroundColor(data);
                }
            }

            return true;
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryFrameName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Frame", data.Row, data.Column);
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryStackName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Stack", data.Row, data.Column);
        }

        /// <summary>
        /// Covert the player map location to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryImageButtonName(MapModelLocation data)
        {
            // Look up the Frame in the Dictionary
            return string.Format("MapR{0}C{1}ImageButton", data.Row, data.Column);
        }

        /// <summary>
        /// Populate the Map
        /// 
        /// For each map position in the Engine
        /// Create a grid object to hold the Stack for that grid cell.
        /// </summary>
        /// <returns></returns>
        public bool CreateMapGridObjects()
        {
            // Make a frame for each location on the map
            // Populate it with a new Frame Object that is unique
            // Then updating will be easier

            foreach (var location in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                var data = MakeMapGridBox(location);

                // Add the Box to the UI

                MapGrid.Children.Add(data, location.Column, location.Row);
            }

            // Set the Height for the MapGrid based on the number of rows * the height of the BattleMapFrame

            var height = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapXAxiesCount * 60;

            BattleMapDisplay.MinimumHeightRequest = height;
            BattleMapDisplay.HeightRequest = height;

            return true;
        }

        /// <summary>
        /// Get the Frame from the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetMapGridObject(string name)
        {
            _ = MapLocationObject.TryGetValue(name, out var data);
            return data;
        }

        /// <summary>
        /// Make the Game Map Frame 
        /// Place the Character or Monster on the frame
        /// If empty, place Empty
        /// </summary>
        /// <param name="mapLocationModel"></param>
        /// <returns></returns>
        public Frame MakeMapGridBox(MapModelLocation mapLocationModel)
        {
            if (mapLocationModel.Player == null)
            {
                mapLocationModel.Player = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare;
            }

            var PlayerImageButton = DetermineMapImageButton(mapLocationModel);

            var PlayerStack = new StackLayout
            {
                Padding = 0,
                Style = (Style)Application.Current.Resources["BattleMapImageBox"],
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = DetermineMapBackgroundColor(mapLocationModel),
                Children = {
                    PlayerImageButton
                },
            };

            _ = MapGridObjectAddImage(PlayerImageButton, mapLocationModel);
            _ = MapGridObjectAddStack(PlayerStack, mapLocationModel);

            var MapFrame = new Frame
            {
                Style = (Style)Application.Current.Resources["BattleMapFrame"],
                Content = PlayerStack,
                AutomationId = GetDictionaryFrameName(mapLocationModel)
            };

            return MapFrame;
        }

        /// <summary>
        /// This add the ImageButton to the stack to kep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddImage(ImageButton data, MapModelLocation MapModel)
        {
            var name = GetDictionaryImageButtonName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);

            return true;
        }

        /// <summary>
        /// This adds the Stack into the Dictionary to keep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddStack(StackLayout data, MapModelLocation MapModel)
        {
            var name = GetDictionaryStackName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);
            return true;
        }

        /// <summary>
        /// Set the Image onto the map
        /// The Image represents the player
        /// 
        /// So a charcter is the character Image for that character
        /// 
        /// The Automation ID equals the guid for the player
        /// This makes it easier to identify when checking the map to update thigns
        /// 
        /// The button action is set per the type, so Characters events are differnt than monster events
        /// </summary>
        /// <param name="MapLocationModel"></param>
        /// <returns></returns>
        public ImageButton DetermineMapImageButton(MapModelLocation MapLocationModel)
        {
            var data = new ImageButton
            {
                Style = (Style)Application.Current.Resources["BattleMapPlayerSmallStyle"],
                Source = MapLocationModel.Player.ImageURI,

                // Store the guid to identify this button
                AutomationId = MapLocationModel.Player.Guid
            };

            switch (MapLocationModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    data.Clicked += (sender, args) => SetSelectedCharacter(MapLocationModel);
                    break;
                case PlayerTypeEnum.Monster:
                    data.Clicked += (sender, args) => SetSelectedMonster(MapLocationModel);
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    data.Clicked += (sender, args) => SetSelectedEmpty(MapLocationModel);

                    // Use the blank cell
                    data.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare.ImageURI;
                    break;
            }

            return data;
        }

        /// <summary>
        /// Set the Background color for the tile.
        /// Monsters and Characters have different colors
        /// Empty cells are transparent
        /// </summary>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public Color DetermineMapBackgroundColor(MapModelLocation MapModel)
        {
            string BattleMapBackgroundColor;
            switch (MapModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    BattleMapBackgroundColor = "BattleMapCharacterColor";
                    break;
                case PlayerTypeEnum.Monster:
                    BattleMapBackgroundColor = "BattleMapMonsterColor";
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
            }

            var result = (Color)Application.Current.Resources[BattleMapBackgroundColor];
            return result;
        }

        #region MapEvents
        /// <summary>
        /// Event when an empty location is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedEmpty(MapModelLocation data)
        {
            // TODO: Info

            /*
             * This gets called when the characters is clicked on
             * Usefull if you want to select the location to move to etc.
             * 
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            return true;
        }

        /// <summary>
        /// Event when a Monster is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedMonster(MapModelLocation data)
        {
            // TODO: Info

            /*
             * This gets called when the Monster is clicked on
             * Usefull if you want to select the monster to attack etc.
             * 
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            data.IsSelectedTarget = true;
            return true;
        }

        /// <summary>
        /// Event when a Character is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedCharacter(MapModelLocation data)
        {
            // TODO: Info

            /*
             * This gets called when the characters is clicked on
             * Usefull if you want to select the character and then set state or do something
             * 
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            return true;
        }
        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Draw the Map
            _ = UpdateMapGrid();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
           // BattlePlayerBoxVersus.Text = "";
           

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
            {
                return;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender == null)
            {
                return;
            }

            if(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Monster)
            {
                MonsterImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ImageURI;
                MonsterName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
                MonsterHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetMaxHealthTotal.ToString();

                CharacterImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI;
                CharacterName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Name;
                CharacterHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetMaxHealthTotal.ToString();

            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                CharacterImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ImageURI;
                CharacterName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
                CharacterHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetMaxHealthTotal.ToString();

                MonsterImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI;
                MonsterName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Name;
                MonsterHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetMaxHealthTotal.ToString();
            }

            // Show what action the Attacker used
            AttackerAttack.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.PreviousAction.ToImageURI();

            var item = ItemIndexViewModel.Instance.GetItem(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand);
            if (item != null)
            {
                AttackerAttack.Source = item.ImageURI;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Alive == false)
            {
                _ = UpdateMapGrid();
                if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender?.PlayerType == PlayerTypeEnum.Monster)
                {
                    MonsterImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI.Replace(".png", ".gif");
                    MonsterImage.IsAnimationPlaying = true;
                }
                
            }
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            MonsterImage.Source = string.Empty;
            MonsterName.Text = string.Empty;
            MonsterHealth.Text = string.Empty;

            CharacterImage.Source = string.Empty;
            CharacterName.Text = string.Empty;
            CharacterHealth.Text = string.Empty;
            CharacterImage.BackgroundColor = Color.Transparent;

           // BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Next Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextButton_Clicked(object sender, EventArgs e)
        {
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
                // Set attacker and defender
                SetAttackerAndDefender();

            var name = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.Text = string.Format("{0} \n{1}", "It's now " + name + "'s turn.", BattleMessages.Text);
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive = true;
                HealButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
                AttackButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
                FocusedButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
                NextButton.IsEnabled = false;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Monster)
                // Take Turn
                NextAttackExample();
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            // Set action to attack
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

            // Take Turn
            NextAttackExample();

            // Set attacker and defender
            //SetAttackerAndDefender();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType != PlayerTypeEnum.Character)
            {
                // Set character active to false
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive = false;

                // Enabled next button
                NextButton.IsEnabled = true;
            }

            var name = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.Text = string.Format("{0} \n{1}", "It's now " + name + "'s turn.", BattleMessages.Text);
            }

            // Enable or disable buttons if attacker is/isn't a character
            HealButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
            AttackButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
            FocusedButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;

        }

        /// <summary>
        /// Heal Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HealButton_Clicked(object sender, EventArgs e)
        {
            // Set action to attack
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Ability;

            // Select heal ability
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentActionAbility = AbilityEnum.Bandage;

            // Take Turn
            NextAttackExample();

            // Set attacker and defender
            //SetAttackerAndDefender();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType != PlayerTypeEnum.Character)
            {
                // Set character active to false
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive = false;

                // Enabled next button
                NextButton.IsEnabled = true;
            }

            var name = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.Text = string.Format("{0} \n{1}", "It's now " + name + "'s turn.", BattleMessages.Text);
            }

            // Enable or disable buttons if attacker is/isn't a character
            HealButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
            AttackButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
            FocusedButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;

        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Setttings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public void NextAttackExample()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }

            // Set new attacker and defender
            SetAttackerAndDefender();
        }

        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender()
        {
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());

            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // User would select who to attack

                    // for now just auto selecting
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;

                case PlayerTypeEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;
            }
        }

        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            BattleMessages.Text = string.Empty;

            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage, BattleMessages.Text);

            //show message if target is killed
            if(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender?.Alive == false)
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnDeathMessage, BattleMessages.Text);
            }
 
            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
            }

        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers
       

        /// <summary>
        /// Click to select to use focused attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FocusedAttackButton_Clicked(object sender, EventArgs e)
        {

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ItemsCount() == 0 ||
                BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.CountFocusedAttackUsed != 0)
            {
                // Set action to attack
                BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

                // Take Turn
                NextAttackExample();
            }
            else
                // Call FocusedAttack
                FocusedAttack();

            // Set attacker and defender
            //SetAttackerAndDefender();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType != PlayerTypeEnum.Character)
            {
                // Set character active to false
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive = false;

                // Enabled next button
                NextButton.IsEnabled = true;
            }

            var name = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.Text = string.Format("{0} \n{1}", "It's now " + name + "'s turn.", BattleMessages.Text);
            }

            // Enable or disable buttons if attacker is/isn't a character
            HealButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
            AttackButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;
            FocusedButton.IsEnabled = BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterActive;

        }

        /// <summary>
        /// Perform a focused attack for attacker
        /// </summary>
        public void FocusedAttack()
        {
           
            //Select action focused attack for this attacker
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.FocusedAttack;

            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }
        }
        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            //_ = await Navigation.PopModalAsync();
            await Navigation.PushModalAsync(new NavigationPage (new GamePage()));
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }
        #endregion PageHandelers

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            OptionBox.IsVisible = false;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }

            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

            // Update the Mode

            ShowBattleModeDisplay();

            ShowBattleModeUIElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUIElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    StartBattleButton.IsVisible = true;
                    break;

                case BattleStateEnum.NewRound:
                    _ = UpdateMapGrid();
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    NextRoundButton.IsVisible = true;
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();

                    // Show the Game Over Display
                    GameOverDisplay.IsVisible = true;
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    OptionBox.IsVisible = true;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }
        }

        /// <summary>
        /// Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                case BattleModeEnum.MapNext:
                    GamePlayersTopDisplay.IsVisible = false;
                    BattleMapDisplay.IsVisible = true;
                    break;

                case BattleModeEnum.SimpleAbility:
                case BattleModeEnum.SimpleNext:
                case BattleModeEnum.Unknown:
                default:
                    GamePlayersTopDisplay.IsVisible = true;
                    BattleMapDisplay.IsVisible = false;
                    break;
            }
        }
    }
}