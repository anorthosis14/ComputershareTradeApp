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

            program.CalculateMaxProfitWithNewInstructions(buyDataDict, sellDataDict);

            if (tradeData != null)
            {
                Console.WriteLine(tradeData.TradeDays());
            }
            else
            {
                Console.WriteLine("No profit could be made");
            }

        }

        /*
         * This is created as an example of accepting a number of datasets
         * and returning the best buy and sell day of each dataset
         */
        public void CalculateMaxProfitWithNewInstructions(Dictionary<int, double> buyDataDict, Dictionary<int, double> sellDataDict)
        {

            Dictionary<int, double>[] dataSets = new Dictionary<int, double>[] {
            buyDataDict,
            sellDataDict
            };

            for (int i = 0; i < dataSets.Length; i++)
            {
                double maxProfit = 0;
                double currentProfit;

                TradeData tradeData = null;
                for (int j = 1; j <= dataSets[i].Count-1; j++)
                {
                    for (int k = j+1; k <= dataSets[i].Count; k++)
                    {
                        currentProfit = dataSets[i][k] - dataSets[i][j];

                        if (currentProfit > maxProfit)
                        {
                            maxProfit = currentProfit;
                            tradeData = new TradeData(dataSets[i].Keys.ElementAt(j - 1), dataSets[i].Keys.ElementAt(k - 1), dataSets[i][j], dataSets[i][k]);
                        }
                    }
                }
                Console.WriteLine(tradeData.TradeDays());
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

            /* 
            * if this was taking only one data set at a time
            * would look like the code below 
            * int j = 1; j <= buyDataDict.Count-1; j++
            * int k = j+1; k <= buyDataDict.Count; k++
            */
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
