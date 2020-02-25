using NUnit.Framework;
using ComputerShareTradeApp;
using System;
using FakeItEasy;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ComputershareTradeAppTests
{
    // Example Test cases
    [TestFixture]
    public class MainProgramTests
    {
        private Program program;

        [SetUp]
        public void Setup()
        {
            program = A.Fake<Program>();
        }

        [TestCase("12.13,14.23,21.2%3")]
        [TestCase("")]
        public void SplitData_Returns_FormatException(string dataSet)
        {
            Assert.Catch<FormatException>(() => program.SplitData(dataSet));
        }

        [TestCase("notExistingFilename")]
        public void ReadData_Returns_IOException(string fileName)
        {
            Assert.Catch<IOException>(() => program.ReadData(fileName));
        }

        [TestCase]
        public void CalculateMaxProfit_Returns_IOException()
        {
            Dictionary<int, double> buyDataDict = new Dictionary<int, double>();
            Dictionary<int, double> sellDataDict = new Dictionary<int, double>();

            buyDataDict.Add(1, 12.23);
            buyDataDict.Add(2, 19.07);
            sellDataDict.Add(1, 23.17);
            sellDataDict.Add(2, 18.56);

            var data =  program.CalculateMaxProfit(buyDataDict,sellDataDict);

            Assert.IsNotNull(data);
            Assert.AreEqual(buyDataDict.Keys.ElementAt(0), data.buyDay, "Invalid buy day returned");
            Assert.AreEqual(sellDataDict.Keys.ElementAt(0), data.sellDay, "Invalid sell day returned");
            Assert.AreEqual(buyDataDict[1], data.buyPrice, "Invalid buy price returned");
            Assert.AreEqual(sellDataDict[1], data.sellPrice, "Invalid sell price returned");
        }
    }
}