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
                    Description = "Has a long reach",
                    ImageURI = "spatula.png",
                    Range = 5,
                    Damage = 4,
                    Value = 3,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},
                
                new ItemModel {
                    Name = "Cookbook",
                    Description = "Turn enemies into food" ,
                    ImageURI = "cookbook.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.MaxHealth},

                new ItemModel {
                    Name = "Rolling Pin",
                    Description = "Can also be used to make cookies",
                    ImageURI = "rollingpin.png",
                    Range = 3,
                    Damage = 6,
                    Value = 4,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},
                
                new ItemModel {
                    Name = "Sneakers",
                    Description = "More speed, less haste",
                    ImageURI = "nonslip.png",
                    Range = 0,
                    Damage = 0,
                    Value = 6,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},
                
                new ItemModel {
                    Name = "Coffee",
                    Description = "Restore your energy",
                    ImageURI = "coffee.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.CurrentHealth},

                new ItemModel {
                    Name = "Chef's Knife",
                    Description = "Handle with care",
                    ImageURI = "knife.png",
                    Range = 4,
                    Damage = 8,
                    Value = 6,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Apron",
                    Description = "Keeps stains at bay",
                    ImageURI = "apron.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},
                
                new ItemModel {
                    Name = "Wooden Spoon",
                    Description = "Not so easy to break",
                    ImageURI = "woodenspoon.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Sponge",
                    Description = "No one ever squeezes the water out",
                    ImageURI = "sponge.png",
                    Range = 0,
                    Damage = 0,
                    Value = 3,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Soap",
                    Description = "Cuts through grease",
                    ImageURI = "soap.png",
                    Range = 0,
                    Damage = 0,
                    Value = 5,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.CurrentHealth},

                 new ItemModel {
                    Name = "Salt",
                    Description = "Rub some salt in it",
                    ImageURI = "salt.png",
                    Range = 8,
                    Damage = 2,
                    Value = 2,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                  new ItemModel {
                    Name = "Vinegar",
                    Description = "Sanitize your cuts",
                    ImageURI = "vinegar.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Head,
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
                    Description = "Gastropub: Combines quality with affordability",
                    Level = 4,
                    MaxHealth = 25,
                    Attack = 7,
                    Defense = 4,
                    Speed = 3,
                    ImageURI = "chefwhite.png",
                    Head = null,
                    Necklass = null,
                    PrimaryHand = null,
                    OffHand = null,
                    Feet = null,
                    RightFinger = null,
                    LeftFinger = null,
                },

                new CharacterModel {
                    Name = "John",
                    Description = "Pastry: Meticulously creates sumptuous treats",
                    Level = 6,
                    MaxHealth = 20,
                    Attack = 4,
                    Defense = 8,
                    Speed = 2,
                    ImageURI = "chefblue.png",
                    Head = null,
                    Necklass = null,
                    PrimaryHand = null,
                    OffHand = null,
                    Feet = null,
                    RightFinger = null,
                    LeftFinger = null,
                },

                new CharacterModel {
                    Name = "Sam",
                    Description = "Fast Food: Fast, cheap, or good; pick two",
                    Level = 1,
                    MaxHealth = 10,
                    Attack = 2,
                    Defense = 4,
                    Speed = 9,
                    ImageURI = "chefyellow.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = null,
                    Feet = null,
                    RightFinger = null,
                    LeftFinger = null,
                },

                new CharacterModel {
                    Name = "Wesley",
                    Description = "Sushi: Fish beware",
                    Level = 8,
                    MaxHealth = 30,
                    Attack = 7,
                    Defense = 6,
                    Speed = 4,
                    ImageURI = "chefblue.png",
                    Head = null,
                    Necklass = null,
                    PrimaryHand = null,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = null,
                    LeftFinger = null,
                },

                new CharacterModel {
                    Name = "Skye",
                    Description = "Macrobiotic: Heal the body and the soul",
                    Level = 4,
                    MaxHealth = 50,
                    Attack = 4,
                    Defense = 9,
                    Speed = 6,
                    ImageURI = "chefgreen.png",
                    Head = null,
                    Necklass = NecklassString,
                    PrimaryHand = null,
                    OffHand = null,
                    Feet = null,
                    RightFinger = null,
                    LeftFinger = LeftFingerString,
                },

                new CharacterModel {
                    Name = "Pierre",
                    Description = "Haute Cuisine: You'll be waiting a while",
                    Level = 9,
                    MaxHealth = 35,
                    Attack = 9,
                    Defense = 4,
                    Speed = 1,
                    ImageURI = "cheforange.png",
                    Head = HeadString,
                    Necklass = null,
                    PrimaryHand = PrimaryHandString,
                    OffHand = null,
                    Feet = null,
                    RightFinger = null,
                    LeftFinger = null,
                },

                new CharacterModel {
                    Name = "Jade",
                    Description = "Vegan: Credits diet for incredible health",
                    Level = 3,
                    MaxHealth = 40,
                    Attack = 5,
                    Defense = 8,
                    Speed = 1,
                    ImageURI = "chefpurple.png",
                    Head = null,
                    Necklass = null,
                    PrimaryHand = null,
                    OffHand = null,
                    Feet = null,
                    RightFinger = null,
                    LeftFinger = null,
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
                    Attack = 7,
                    Defense = 1,
                    Speed = 3,
                    ImageURI = "eggs.png",
                },

                new MonsterModel {
                    Name = "Spiny Pineapple",
                    Description = "It'll prick you",
                    Attack = 5,
                    Defense = 7,
                    Speed = 1,
                    ImageURI = "pineapple.png",
                },

                new MonsterModel {
                    Name = "Garlic",
                    Description = "Its breath will strike you dead",
                    Attack = 7,
                    Defense = 1,
                    Speed = 4,
                    ImageURI = "garlic.png",
                },

                new MonsterModel {
                    Name = "Lettuce",
                    Description = "Now with extra E. coli",
                    Attack = 1,
                    Defense = 4,
                    Speed = 7,
                    ImageURI = "lettuce.png",
                },

                new MonsterModel {
                    Name = "Steak",
                    Description = "Things might get bloody",
                    Attack = 6,
                    Defense = 4,
                    Speed = 2,
                    ImageURI = "steak.png",
                },

                new MonsterModel {
                    Name = "Spicy Strawberry",
                    Description = "Zesty",
                    Attack = 1,
                    Defense = 3,
                    Speed = 6,
                    ImageURI = "strawberry.png",
                },

                new MonsterModel {
                    Name = "Sour Watermelon",
                    Description = "Watch out for seeds",
                    Attack = 1,
                    Defense = 6,
                    Speed = 4,
                    ImageURI = "watermelon.png",
                },

                new MonsterModel {
                    Name = "Fast Carrots",
                    Description = "Very orange!",
                    Attack = 5,
                    Defense = 3,
                    Speed = 7,
                    ImageURI = "carrots.png",
                },

                new MonsterModel {
                    Name = "Diva Eggplant",
                    Description = "Born to dance",
                    Attack = 5,
                    Defense = 1,
                    Speed = 3,
                    ImageURI = "eggplant.png",
                },

                new MonsterModel {
                    Name = "Donut Boss",
                    Description = "You'll never see it coming",
                    Attack = 8,
                    Defense = 7,
                    Speed = 9,
                    ImageURI = "donut_boss.png",
                },

                new MonsterModel {
                    Name = "Pizza Boss",
                    Description = "Spicy",
                    Attack = 8,
                    Defense = 9,
                    Speed = 7,
                    ImageURI = "pizza_boss.png",
                },

                new MonsterModel {
                    Name = "Sushi Boss",
                    Description = "Something's fishy . . .",
                    Attack = 9,
                    Defense = 7,
                    Speed = 8,
                    ImageURI = "sushi_boss.png",
                },
            };

            return datalist;
        }
    }
}