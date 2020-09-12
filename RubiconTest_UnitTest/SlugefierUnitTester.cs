using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiconTest.Infrastructure.Shared;

namespace RubiconTest_UnitTest
{
    [TestClass]
    public class SlugefierUnitTester
    {


        [TestMethod]
        public void Return_EmptyString_If_String_empty()
        {
            // Arrange


            //Act
            var result = Slugefier.GetFriendlyTitle("");


            //Assert
            Assert.IsTrue(result=="");


        }

        [TestMethod]
        public void Return_Succesfull_Slug()
        {

            // Arrange
            string result = "";

            //Act
            result = Slugefier.GetFriendlyTitle("Test slug for testing");


            //Assert
            Assert.IsTrue(!result.Contains(" "));
        }
    }
}
