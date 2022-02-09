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
                    Description = "Has a long reach.",
                    ImageURI = "sword1.png",
                    Range = 1,
                    Damage = 4,
                    Value = 6,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Cookbook",
                    Description = "Turn enemies into food (Maximum Health)" ,
                    ImageURI = "sword2.png",
                    Range = 0,
                    Damage = 3,
                    Value = 5,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Rolling Pin",
                    Description = "Can also be used to make cookies (Attack)",
                    ImageURI = "sword3.png",
                    Range = 0,
                    Damage = 6,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Mixer",
                    Description = "Cook more in less time (Speed)",
                    ImageURI = "sword4.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Thermometer",
                    Description = "Cook more in less time (Speed)",
                    ImageURI = "sword6.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Sneakers",
                    Description = "More speed, less haste (Speed)",
                    ImageURI = "sword7.png",
                    Range = 0,
                    Damage = 1,
                    Value = 4,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Coffee",
                    Description = "Restore your energy (Current Health)",
                    ImageURI = "sword8.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Snack",
                    Description = "Restore your energy (Current Health)",
                    ImageURI = "sword9.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Chef's Hat",
                    Description = "For the more experienced (Maximum Health)",
                    ImageURI = "sword10.png",
                    Range = 0,
                    Damage = 0,
                    Value = 7,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Hair Net",
                    Description = "(Defense)",
                    ImageURI = "sword11.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Ice",
                    Description = "Put some on that burn (Current Health)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Bib",
                    Description = "(Defense)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Bow Tie",
                    Description = "(Maximum Health)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Chef's Knife",
                    Description = "Handle with care (Attack)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 7,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Torch",
                    Description = "Light it up (Attack)",
                    ImageURI = "shield1.png",
                    Range = 5,
                    Damage = 7,
                    Value = 8,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Spray Bottle",
                    Description = "(Attack)",
                    ImageURI = "shield1.png",
                    Range = 7,
                    Damage = 1,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Chef's Jacket",
                    Description = "(Maximum Health)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 8,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Apron",
                    Description = "Keeps stains at bay (Defense)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Tank Top",
                    Description = "Doesn't bog you down (Speed)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Rubber Gloves",
                    Description = "(Speed)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Oven Mitts",
                    Description = "(Defense)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Hand Lotion",
                    Description = "(Current Health)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Rubber Boots",
                    Description = "(Defense)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 2,
                    Value = 6,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Loafers",
                    Description = "(Maximum Health)",
                    ImageURI = "shield1.png",
                    Range = 0,
                    Damage = 0,
                    Value = 6,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.MaxHealth}

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