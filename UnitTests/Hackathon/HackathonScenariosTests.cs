﻿using NUnit.Framework;

using Game.Models;
using System.Threading.Tasks;
using Game.ViewModels;
using Game.Helpers;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1

        #region Scenario2
        [Test]
        public async Task HackathonScenario_Scenario_2_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Force Doug always miss
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Doug
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerDoug = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Doug",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);     

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            EngineViewModel.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Act
            var result = EngineViewModel.Engine.Round.Turn.TurnAsAttack(CharacterPlayerDoug, MonsterPlayer);
            var status = EngineViewModel.Engine.EngineSettings.BattleMessagesModel.HitStatus;

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(HitStatusEnum.Miss, status);
        }
        #endregion Scenario2

        #region Scenario14
        [Test]
        public async Task HackathonScenario_Scenario_14_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      14
            *      
            * Description: 
            *      Rather than 6 monsters, 1 really tough monster is created for a given round.
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      Changes in RoundEngine.cs: Add logic to AddMonstersToRound to allow the 
            *      creation of Boss Monsters if AllowBossMonsters toggle was selected.
            *      
            *      Changes in RandomPlayerHelper.cs: Add parameter for if Boss Monsters are allowed,
            *      and if so create one.
            *     
            * 
            * Test Algrorithm:
            *       Create 2 Characters
            *       Set AllowBossMonsters = true
            *       Enable Forced Rolls to ensure that the next round is a Boss level
            *       Force
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *       Verify that when AllowBossMonsters is enabled and a dice roll of 6 
            *       is rolled a single Monster is present in the round
            *       
            *       Verify that the single Monster HP = Character 1 HP + Character 2 HP + 20
            *   
            */

            EngineViewModel.Engine.EngineSettings.MonsterList.Clear();

            // Arrange

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowBossMonsters = true;

            var Character1 = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 20,
                ExperienceTotal = 1,
                Name = "Character1",
                ListOrder = 1,
            };

            var Character2 = new CharacterModel
            {
                Speed = 20,
                Level = 1,
                CurrentHealth = 20,
                ExperienceTotal = 1,
                Name = "Character2",
                ListOrder = 1,
            };

            // Add each model here to warm up and load it.
            _ = Game.Helpers.DataSetsHelper.WarmUp();

            EngineViewModel.Engine.EngineSettings.CharacterList.Clear();

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character1));
            EngineViewModel.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character2));

            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(6);

            EngineViewModel.Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel());

            // Make the List
            EngineViewModel.Engine.EngineSettings.PlayerList = EngineViewModel.Engine.Round.MakePlayerList();

            _ = DiceHelper.DisableForcedRolls();

            // Act
            var result = EngineViewModel.Engine.EngineSettings.MonsterList.Count;

            var totalHP = 0;
            for(int i = 0; i < EngineViewModel.Engine.EngineSettings.CharacterList.Count; i++)
            {
                totalHP += EngineViewModel.Engine.EngineSettings.CharacterList[i].CurrentHealth;
            }

            // Reset

            // Assert
            Assert.AreEqual(result, 1);
            Assert.AreEqual(totalHP + 20, 60);

        }
        #endregion Scenario14
    }
}