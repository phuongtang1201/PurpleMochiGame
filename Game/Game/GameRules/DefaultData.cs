using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Spatula",
                    Description = "Gold Spatula increases chef's attack ability.",
                    ImageURI = "sword1.png",
                    Range = 0,
                    Damage = 3,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Cookbook",
                    Description = "Records all secrets of ingredients. Increase chef's defense." ,
                    ImageURI = "sword2.png",
                    Range = 3,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Rolling pin",
                    Description = "Powerfull weapon. Increase chef's attack",
                    ImageURI = "sword3.png",
                    Range = 4,
                    Damage = 4,
                    Value = 5,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Mixer",
                    Description = "Chef can move faster",
                    ImageURI = "sword4.png",
                    Range = 2,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Thermometer",
                    Description = "Protects chef from high temperature.",
                    ImageURI = "sword6.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Shoes",
                    Description = "Chef can move extremely fast",
                    ImageURI = "sword7.png",
                    Range = 1,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Coffee",
                    Description = "Increases chef's speed by getting more energy.",
                    ImageURI = "sword8.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Snack",
                    Description = "Increases chef's current health by getting more energy.",
                    ImageURI = "sword9.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Splatter",
                    Description = "Increases ingredient's attack",
                    ImageURI = "sword10.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Mold",
                    Description = "Increases ingredient's defense",
                    ImageURI = "sword11.png",
                    Range = 1,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ice",
                    Description = "Increases ingredient's health",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.CurrentHealth},

            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "First Score",
                    Description = "Test Data",
                },

                new ScoreModel {
                    Name = "Second Score",
                    Description = "Test Data",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Lisa",
                    Description = "Brings sweetness to everyone by her dessert",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "elf1.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "John",
                    Description = "Can make delicious cake.",
                    Level = 1,
                    MaxHealth = 5,
                    ImageURI = "elf2.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Sam",
                    Description = "Go to get yummy Italian pizza.",
                    Level = 1,
                    MaxHealth = 8,
                    ImageURI = "elf4.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Sue",
                    Description = "Brings premium sushi with line chef level",
                    Level = 4,
                    MaxHealth = 38,
                    ImageURI = "elf3.png"
                },

                new CharacterModel {
                    Name = "Nick",
                    Description = "Makes fresh vegan food.",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "elf5.png"
                },

                new CharacterModel {
                    Name = "Bob",
                    Description = "A pasta chef",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "elf6.png"
                },

                new CharacterModel {
                    Name = "Jade",
                    Description = "A gastropub chef",
                    Level = 5,
                    MaxHealth = 43,
                    ImageURI = "elf7.png"
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Rotten Egg",
                    Description = "Small and smelly",
                    ImageURI = "troll1.png",
                },

                new MonsterModel {
                    Name = "Spiny Pineapple",
                    Description = "It'll prick you",
                    ImageURI = "troll2.png",
                },

                new MonsterModel {
                    Name = "Moldy Rice",
                    Description = "Death on impact",
                    ImageURI = "troll3.png",
                },

                new MonsterModel {
                    Name = "Yogurt",
                    Description = "May the Schwartz be with you",
                    ImageURI = "troll4.png",
                },

                new MonsterModel {
                    Name = "Gefilte Fish",
                    Description = "A taste even a mom can't love",
                    ImageURI = "troll5.png",
                },

                new MonsterModel {
                    Name = "Spicy Strawberry",
                    Description = "Zesty",
                    ImageURI = "troll6.png",
                },
            };

            return datalist;
        }
    }
}