using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiconTest.Infrastructure.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconTest_UnitTest
{
    //Test class yous for unit testing the data generation functions of the "DataGenerator" class
    [TestClass]
    public class GeneratorUnitTester
    {

        DataGeneration dataGeneration = new DataGeneration();

        [TestMethod]
        public void Should_Return_Bool()
        {
            // Arrange


            // Act
            var result = dataGeneration.RandomBit();

            // Assert
            Assert.IsInstanceOfType(result, typeof(bool));

        }
        [TestMethod]
        [DataRow(2)]
        [DataRow(17)]
        [DataRow(44)]
        public void Should_Generate_String_SpecificLenght(int len)
        {
            // Arrange
            int result = -1;

            // Act
            result = dataGeneration.GenerateString(len).Length;

            // Assert
            Assert.AreEqual(result, len);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(-2, -3)]
        [DataRow(7, -3)]
        [DataRow(2, 2)]
        public void Should_Throw_Excpetion_Bad_Input_Data(int min, int max)
        {
            // Arrange

            // Act

            // Assert
            dataGeneration.GenerateStringRandomLenght(min, max);
        }


        [TestMethod]
        [DataRow(2, 20)]
        [DataRow(4, 40)]
        [DataRow(22, 30)]
        public void Should_Generate_Random_Lenghts_String_Inside_Parama_Range(int min, int max)
        {

            // Arrange
            int lenght = -1;

            // Act
            lenght = dataGeneration.GenerateStringRandomLenght(min, max).Length;

            // Assert
            Assert.IsTrue(lenght >= min && lenght <= max);


        }

        [TestMethod]
        public void Should_Return_Date_In_Five_Year_Range()
        {

            // Arrange
            DateTime MinDate = new DateTime(DateTime.UtcNow.Year - 5, 1, 1);

            // Act
            DateTime generatedDate = dataGeneration.RandomDate();

            // Assert
            Assert.IsTrue(generatedDate < DateTime.Now && generatedDate > MinDate);

        }




    }
}
