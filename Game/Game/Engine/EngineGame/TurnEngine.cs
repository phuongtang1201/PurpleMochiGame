using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Helpers;
using Game.ViewModels;
using Game.GameRules;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;

namespace Game.Engine.EngineGame
{
    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : EngineBase.TurnEngineBase, ITurnEngineInterface
    {
        #region Algrorithm
        /* 
            Need to decide who takes the next turn
            Target to Attack
            Should Move, or Stay put (can hit with weapon range?)
            Death
            Manage Round...
          
            Attack or Move
            Roll To Hit
            Decide Hit or Miss
            Decide Damage
            Death
            Drop Items
            Turn Over
        */
        #endregion Algrorithm

        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.
            // Attack is default; Attack is also used if Move returns false

            var result = false;

            // If the action is not set, then try to set it or use Attack
            if (EngineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                EngineSettings.CurrentAction = DetermineActionChoice(Attacker);

                // When in doubt, attack...
                if (EngineSettings.CurrentAction == ActionEnum.Unknown)
                {
                    EngineSettings.CurrentAction = ActionEnum.Attack;
                }
            }

            switch (EngineSettings.CurrentAction)
            {
                case ActionEnum.FocusedAttack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                // If ability was chosen, use bandage if health less than 3,
                // If speed less than 5, use nimble
                // If defense less than 5, use toughness
                // Otherwise, use Focus
                case ActionEnum.Ability:
                    if (EngineSettings.CurrentActionAbility == AbilityEnum.Unknown || EngineSettings.CurrentActionAbility == AbilityEnum.None)
                    {
                        if (Attacker.GetCurrentHealth() < 3)
                            EngineSettings.CurrentActionAbility = AbilityEnum.Bandage;
                        else if (Attacker.GetSpeed() < 5)
                            EngineSettings.CurrentActionAbility = AbilityEnum.Nimble;
                        else if (Attacker.GetDefense() < 5)
                            EngineSettings.CurrentActionAbility = AbilityEnum.Toughness;
                        else
                            EngineSettings.CurrentActionAbility = AbilityEnum.Focus;
                    }
                    else if (EngineSettings.CurrentActionAbility == AbilityEnum.Heal && Attacker.PlayerType != PlayerTypeEnum.Monster)
                    {
                        result = ChooseToUseHeal(Attacker);
                        if (result)
                            UseAbility(Attacker);
                        else
                        {
                            Attack(Attacker);
                            result = true;
                        }
                        break;
                    }
                    result = UseAbility(Attacker);
                    break;

                // If character can't move, choose attack
                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    if (!result)
                        result = Attack(Attacker);
                    break;
            }

            EngineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
        }

        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {
            /*
             * Attacker will move as close to a Defender as necessary based on the
             * Attacker's range.
             * If an Attacker doesn't need to move or can't move to a closer location,
             * return false
             */

            // For Attack, Choose Whom
            EngineSettings.CurrentDefender = AttackChoice(Attacker);

            if (EngineSettings.CurrentDefender == null)
                return false;

            // Check if Defender is already in range
            if (EngineSettings.MapModel.IsTargetInRange(Attacker, EngineSettings.CurrentDefender))
                return false;

            // Get X, Y for Defender and Attacker
            var locationDefender = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentDefender);
            if (locationDefender == null)
                return false;

            var locationAttacker = EngineSettings.MapModel.GetLocationForPlayer(Attacker);
            if (locationAttacker == null)
                return false;

            // Find Location Nearest to Defender that is Open.
            // Get the Open Locations
            var openSquare = EngineSettings.MapModel.ReturnClosestEmptyLocation(locationDefender);

            Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row));

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + " moves closer to " + EngineSettings.CurrentDefender.Name;

            return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, openSquare);
        }

        /// <summary>
        /// Decide to use an Ability or not
        /// 
        /// Set the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool ChooseToUseAbility(PlayerInfoModel Attacker)
        {
            // See if healing is needed.
            EngineSettings.CurrentActionAbility = Attacker.SelectHealingAbility();
            if (EngineSettings.CurrentActionAbility != AbilityEnum.Unknown)
            {
                EngineSettings.CurrentAction = ActionEnum.Ability;
                return true;
            }

            // If not needed, find attacker's greatest weakness, then roll dice to see if
            // ability should be applied; default is to roll dice for attack
            if (Attacker.GetSpeed() < Attacker.GetDefense() && Attacker.GetSpeed() < Attacker.GetAttack())
            {
                if (DiceHelper.RollDice(1, 10) < 4)
                {
                    EngineSettings.CurrentActionAbility = AbilityEnum.Nimble;
                    EngineSettings.CurrentAction = ActionEnum.Ability;
                    return true;
                }
                return false;
            }
            else if (Attacker.GetDefense() < Attacker.GetSpeed() && Attacker.GetDefense() < Attacker.GetAttack())
            {
                if (DiceHelper.RollDice(1, 10) < 3)
                {
                    EngineSettings.CurrentActionAbility = AbilityEnum.Toughness;
                    EngineSettings.CurrentAction = ActionEnum.Ability;
                    return true;
                }
                return false;
            }
            else
            {
                if (DiceHelper.RollDice(1, 10) < 2)
                {
                    EngineSettings.CurrentActionAbility = AbilityEnum.Focus;
                    EngineSettings.CurrentAction = ActionEnum.Ability;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Decide to use an Ability or not
        /// 
        /// Set the Ability
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public bool ChooseToUseHeal(PlayerInfoModel Attacker)
        {
            // If current health down by 50%, increase current health by 50%
            if (Attacker.CurrentHealth < Attacker.MaxHealth / 2)
            {
                var moreHP = Attacker.CurrentHealth / 2;
                Attacker.CurrentHealth += moreHP;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select first in the list

            // Select who have fewest items first
            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character)
                .OrderBy(m => m.ItemsCount())
                .FirstOrDefault();
            
            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the lowest defense monster first 

            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderBy(m => m.Defense).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }
            if(EngineSettings.CurrentAction == ActionEnum.FocusedAttack)
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
                // It's a Hit

                //Calculate Damage
                EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                //10X damage
                EngineSettings.BattleMessagesModel.DamageAmount *= 10;

                //Increment the CountFocusedAttackedUsed
                Attacker.CountFocusedAttackUsed++;

                // Apply the Damage
                _ = ApplyDamage(Target);

                EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                // Check if Dead and Remove
                _ = RemoveIfDead(Target);

                // If it is a character apply the experience earned
                _ = CalculateExperience(Attacker, Target);


                //Dropped the lowest value item
                var droppedItem = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.DropLowestValueItem();
                Debug.WriteLine("Uses Focused attack and drop " + droppedItem.Name.ToString());

                EngineSettings.BattleMessagesModel.AttackStatus = " uses Focused Attack to ";
                EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.AttackStatus + Target.Name + " and drop "+ droppedItem.Name.ToString()+ " forever. " + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
                Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

                return true;

        }

            // Set Messages to empty
            _ = EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            _ = CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            //Force Doug to always miss
            if (Attacker.Name == "Doug")
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
            }

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    _ = DetermineCriticalMissProblem(Attacker);

                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // If critical Hit, double the damage
                    if (EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                    }

                    // Apply the Damage
                    _ = ApplyDamage(Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    _ = RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    _ = CalculateExperience(Attacker, Target);

                    break;
            }

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.AttackStatus + Target.Name + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// Reset dead Monster to alive with half health
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        public bool ReviveMonster(PlayerInfoModel Target)
        {
            Target.Alive = true;
            Target.CurrentHealth = Target.MaxHealth / 2;
            Target.Name = "Zombie " + Target.Name;
            EngineSettings.MapModel.AddPlayerToMap(Target);
                //EngineSettings.MonsterList.Add(Target);
            return true;
        }
        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {
            bool found;

            // Mark Status in output
            EngineSettings.BattleMessagesModel.TurnMessageSpecial = string.Empty;

            //Notify the target is killed
            EngineSettings.BattleMessagesModel.TurnDeathMessage = Target.Name + " has been cooked!";



            // Removing the 
            _ = EngineSettings.MapModel.RemovePlayerFromMap(Target);

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:

                    // Add the Character to the killed list
                    EngineSettings.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.CharacterModelDeathList.Add(Target);

                    _ = DropItems(Target);

                    found = EngineSettings.CharacterList.Remove(EngineSettings.CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                    //default:
                    var d20 = DiceHelper.RollDice(1, 20);
                    // 20% chance to revive
                    if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.AllowZombieMonsters
                        && d20 < 4)
                    {
                        return ReviveMonster(Target);
                    }

                        // Add one to the monsters killed count...
                        EngineSettings.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    EngineSettings.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.MonsterModelDeathList.Add(Target);

                    _ = DropItems(Target);

                    found = EngineSettings.MonsterList.Remove(EngineSettings.MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                default:
                    return false;

                
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(EngineSettings.BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                EngineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            EngineSettings.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            EngineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            EngineSettings.BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to miss ";

                if (EngineSettings.BattleSettingsModel.AllowCriticalMiss)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                }

                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for hit ";
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

                if (EngineSettings.BattleSettingsModel.AllowCriticalHit)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                }
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.DamageAmount = 0;
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        public override List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // Roll of dice depends on number of characters and the round
            // Lower round and more characters => a greater range for dice roll
            // Idea is to give more help with more characters, but have the help
            // decrease the longer the game goes on
            var partyCount = EngineSettings.CharacterList.Count();
            var drops = partyCount - round;
            if (drops <= 0)
                drops = 1;
            if (round > 20)
                drops = 0;

            // The Number drop can be up to the calculated value 
            // Will drop something until round 20
            // Negative results in nothing dropped
            var NumberToDrop = (DiceHelper.RollDice(1, drops));

            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                result.Add(data);
            }

            return result;
        }

        /// <summary>
        /// Determine what Actions to do
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            //Autobattle players will choose to perform Focused Attack if the Monster they are
            //fighting has current health of more than 5x the damage they can deal with an attack,
            //and they have an item equipped
            if (EngineSettings.BattleScore.AutoBattle == true)
            {
                // For Attack, Choose Who
                EngineSettings.CurrentDefender = AttackChoice(Attacker);

                if (EngineSettings.CurrentDefender == null)
                {
                    return ActionEnum.Attack;
                }
                string DamgeString = EngineSettings.CurrentDefender.GetDamageTotalString;
                
                if (EngineSettings.CurrentDefender.CurrentHealth > (int.Parse(DamgeString)* 5))
                {
                    if (Attacker.ItemsCount() > 0)
                    {
                        EngineSettings.CurrentAction = ActionEnum.FocusedAttack;
                        return EngineSettings.CurrentAction;
                    }
                }
            }

            return base.DetermineActionChoice(Attacker);
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return base.DetermineCriticalMissProblem(attacker);
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            return base.BattleSettingsOverride(Attacker);
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            return base.BattleSettingsOverrideHitStatusEnum(myEnum);
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override bool ApplyDamage(PlayerInfoModel Target)
        {
            return base.ApplyDamage(Target);
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateAttackStatus(Attacker, Target);
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            return base.CalculateExperience(Attacker, Target);
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {
            return base.RemoveIfDead(Target);
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        public override bool UseAbility(PlayerInfoModel Attacker)
        {
            return base.UseAbility(Attacker);
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        public override bool Attack(PlayerInfoModel Attacker)
        {
            return base.Attack(Attacker);
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            return base.AttackChoice(data);
        }
    }
}