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
                    ImageURI = "spatula.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Cookbook",
                    Description = "Turn enemies into food (Maximum Health)" ,
                    ImageURI = "cookbook.png",
                    Range = 0,
                    Damage = 3,
                    Value = 5,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Rolling Pin",
                    Description = "Can also be used to make cookies (Attack)",
                    ImageURI = "rollingpin.png",
                    Range = 0,
                    Damage = 6,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Sneakers",
                    Description = "More speed, less haste (Speed)",
                    ImageURI = "nonslip.png",
                    Range = 0,
                    Damage = 1,
                    Value = 4,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Coffee",
                    Description = "Restore your energy (Current Health)",
                    ImageURI = "coffee.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Chef's Knife",
                    Description = "Handle with care (Attack)",
                    ImageURI = "knife.png",
                    Range = 0,
                    Damage = 0,
                    Value = 7,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Apron",
                    Description = "Keeps stains at bay (Defense)",
                    ImageURI = "apron.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Wooden Spoon",
                    Description = "(Defense)",
                    ImageURI = "woodenspoon.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Sponge",
                    Description = "(Defense)",
                    ImageURI = "sponge.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.RightFinger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Soap",
                    Description = "(Defense)",
                    ImageURI = "soap.png",
                    Range = 0,
                    Damage = 2,
                    Value = 6,
                    Location = ItemLocationEnum.LeftFinger,
                    Attribute = AttributeEnum.Defense},

                 new ItemModel {
                    Name = "Salt",
                    Description = "Attack",
                    ImageURI = "salt.png",
                    Range = 4,
                    Damage = 2,
                    Value = 6,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                  new ItemModel {
                    Name = "Vinegar",
                    Description = "(Maximum Health)",
                    ImageURI = "vinegar.png",
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
                    Name = "Linda",
                    Description = "Combines quality with affordability (Gastropub)",
                    Level = 4,
                    MaxHealth = 30,
                    Attack = 6,
                    Defense = 4,
                    Speed = 3,
                    ImageURI = "chefblue.png",
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
                    Description = "Meticulously creates sumptuous treats (Pastry)",
                    Level = 6,
                    MaxHealth = 35,
                    Attack = 7,
                    Defense = 4,
                    Speed = 2,
                    ImageURI = "chefblue.png",
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
                    Description = "(Fast Food)",
                    Level = 1,
                    MaxHealth = 10,
                    Attack = 1,
                    Defense = 3,
                    Speed = 7,
                    ImageURI = "chefyellow.png",
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
                    Description = "Fish beware (Sushi)",
                    Level = 8,
                    MaxHealth = 40,
                    Attack = 7,
                    Defense = 4,
                    Speed = 3,
                    ImageURI = "chefwhite.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Skye",
                    Description = "(Macrobiotic)",
                    Level = 4,
                    MaxHealth = 50,
                    Attack = 5,
                    Defense = 8,
                    Speed = 2,
                    ImageURI = "chefgreen.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Pierre",
                    Description = "You'll be waiting a while (Haute Cuisine)",
                    Level = 9,
                    MaxHealth = 40,
                    Attack = 8,
                    Defense = 5,
                    Speed = 1,
                    ImageURI = "chefpurple.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Jade",
                    Description = "Credits diet for incredible health (Vegan)",
                    Level = 3,
                    MaxHealth = 40,
                    Attack = 4,
                    Defense = 8,
                    Speed = 3,
                    ImageURI = "cheforange.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
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
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "eggs.png",
                },

                new MonsterModel {
                    Name = "Spiny Pineapple",
                    Description = "It'll prick you",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "pineapple.png",
                },

                new MonsterModel {
                    Name = "Garlic",
                    Description = "Death on impact",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "garlic.png",
                },

                new MonsterModel {
                    Name = "Lettuce",
                    Description = "May the Schwartz be with you",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "lettuce.png",
                },

                new MonsterModel {
                    Name = "Steak",
                    Description = "A taste even a mom can't love",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "steak.png",
                },

                new MonsterModel {
                    Name = "Spicy Strawberry",
                    Description = "Zesty",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "strawberry.png",
                },

                new MonsterModel {
                    Name = "Sour Watermelon",
                    Description = "Zesty",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "watermelon.png",
                },

                new MonsterModel {
                    Name = "Fast Carrots",
                    Description = "Very orange!",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "carrots.png",
                },

                new MonsterModel {
                    Name = "Diva Eggplant",
                    Description = "Born to dance",
                    Attack = 3,
                    Defense = 3,
                    Speed = 3,
                    ImageURI = "eggplant.png",
                },

                new MonsterModel {
                    Name = "Donut Boss",
                    Description = "You'll never see it coming",
                    Attack = 7,
                    Defense = 7,
                    Speed = 7,
                    ImageURI = "donut_boss.png",
                },

                new MonsterModel {
                    Name = "Pizza Boss",
                    Description = "Spicy",
                    Attack = 7,
                    Defense = 7,
                    Speed = 7,
                    ImageURI = "pizza_boss.png",
                },

                new MonsterModel {
                    Name = "Sushi Boss",
                    Description = "Something's fishy . . .",
                    Attack = 7,
                    Defense = 7,
                    Speed = 7,
                    ImageURI = "sushi_boss.png",
                },
            };

            return datalist;
        }
    }
}