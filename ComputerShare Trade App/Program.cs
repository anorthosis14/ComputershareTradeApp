using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputerShareTradeApp
{

    public class Program
    {
        static void Main()
        {
            Program program = new Program();

            // Assuming that the buy data is file ChallengeSampleDataSet1
            string buyData = program.ReadData("ChallengeSampleDataSet1");

            // Assuming that the sell data is file ChallengeSampleDataSet2
            string sellData = program.ReadData("ChallengeSampleDataSet2");

            Dictionary<int, double> buyDataDict = program.SplitData(buyData);
            Dictionary<int, double> sellDataDict = program.SplitData(sellData);

            if (buyDataDict == null || sellDataDict == null)
            {
                Console.WriteLine("Insufficient data provided");
                Console.ReadLine();

                return;
            }

            var tradeData = program.CalculateMaxProfit(buyDataDict, sellDataDict);

            if (tradeData != null)
            {
                Console.WriteLine(tradeData.TradeDays());
            }
            else
            {
                Console.WriteLine("No profit could be made");
            }

        }

        public Dictionary<int, double> SplitData(string dataSet)
        {
            Dictionary<int, double> dictionary = new Dictionary<int, double>();

            List<double> values = new List<double>();

            try
            {
                values = dataSet.Split(",").Select(double.Parse).ToList();
            }
            catch (FormatException formatEx)
            {
                throw new FormatException("Invalid data", formatEx.InnerException);
            }

            if (values.Count == 0)
            {
                return null;
            }

            for (int i = 0; i < values.Count; i++)
            {
                dictionary.Add(i + 1, values[i]);
            }

            return dictionary;
        }

        public string ReadData(string fileName)
        {
            try
            {
                string dataSet = File.ReadAllText(@$"..//..//..//{fileName}.txt");

                return dataSet;
            }
            catch (IOException message)
            {
                throw new IOException("Could not read the file", message.InnerException);
            }
        }

        public TradeData CalculateMaxProfit(Dictionary<int, double> buyDataDict, Dictionary<int, double> sellDataDict)
        {
            double maxProfit = 0;
            double currentProfit;

            TradeData tradeData = null;

            for (int j = 1; j <= buyDataDict.Count; j++)
            {
                for (int k = j; k <= sellDataDict.Count; k++)
                {
                    currentProfit = sellDataDict[k] - buyDataDict[j];

                    if (currentProfit > maxProfit)
                    {
                        maxProfit = currentProfit;
                        tradeData = new TradeData(buyDataDict.Keys.ElementAt(j - 1), sellDataDict.Keys.ElementAt(k - 1), buyDataDict[j], sellDataDict[k]);
                    }
                }
            }

            return tradeData;
        }
    }
}
