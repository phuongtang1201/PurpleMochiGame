using NUnit.Framework;

using Game.Models;
using Game.Helpers;

namespace UnitTests.Helpers
{
    [TestFixture]
    class StringEnumConverterHelperTests
    {
        [Test]
        public void StringEnumConvert_String_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Feet";
            var Result = myConverter.Convert(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.Feet;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvert_Enum_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = ItemLocationEnum.Feet;
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = 40;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvert_Other_Should_Skip()
        {
            var myConverter = new StringEnumConverter();

            var myObject = new ItemModel();
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        // Convert Back
        [Test]
        public void IntEnumConvertBack_Should_Skip()
        {
            var myConverter = new IntEnumConverter();

            var myObject = "Bogus";
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        // Convert Back
        [Test]
        public void IntEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new IntEnumConverter();

            var myObject = 40;
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = "Feet";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Feet";
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.Feet;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Current_Health_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Current Health";
            var Result = myConverter.ConvertBack(myObject, typeof(AttributeEnum), null, null);
            var Expected = AttributeEnum.CurrentHealth;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Max_Health_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Max Health";
            var Result = myConverter.ConvertBack(myObject, typeof(AttributeEnum), null, null);
            var Expected = AttributeEnum.MaxHealth;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Necklace_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Necklace";
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.Necklass;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Primary_Hand_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Primary Hand";
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.PrimaryHand;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Off_Hand_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Off Hand";
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.OffHand;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Right_Pocket_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Right Pocket";
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.RightFinger;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_String_Left_Pocket_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Left Pocket";
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.LeftFinger;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_Enum_Should_Skip()
        {
            var myConverter = new StringEnumConverter();

            var myObject = ItemLocationEnum.Feet;
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_Other_Should_Skip()
        {
            var myConverter = new StringEnumConverter();

            var myObject = new ItemModel();
            var Result = myConverter.ConvertBack(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvertBack_Int_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = 40;
            var Result = myConverter.ConvertBack(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = "Feet";

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }
    }
}