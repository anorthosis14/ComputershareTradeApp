namespace ComputerShareTradeApp
{
    public class TradeData
    {
        public int buyDay,sellDay;
        public double buyPrice, sellPrice;

        public TradeData(int dayOfBuy, int dayOfSell, double priceOfBuy, double priceOfSell)
        {
            buyDay = dayOfBuy;
            sellDay = dayOfSell;
            buyPrice = priceOfBuy;
            sellPrice = priceOfSell;
        }    

        public string TradeDays()
        {
            return $"{buyDay}({buyPrice}),{sellDay}({sellPrice})";
        }
    }
}
