namespace RealestateScraper.Core
{
    public class RealestateResult
    {
        public RealestateResult(decimal price)
        {
            Price = price;
        }

        public decimal Price { get; }
    }
}