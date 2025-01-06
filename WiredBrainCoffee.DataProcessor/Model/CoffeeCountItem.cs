namespace WiredBrainCoffee.DataProcessor.Model
{
    // public record CoffeeCountItem(string CoffeeType, int Count);
    public class CoffeeCountItem
    {
        public CoffeeCountItem(string coffeeType, int count)
        {
            CoffeeType = coffeeType;
            Count = count;
        }

        public string CoffeeType { get; }

        public int Count { get; }
    }
}
