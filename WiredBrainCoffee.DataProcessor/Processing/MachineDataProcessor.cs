using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Processing
{
    public class MachineDataProcessor
    {
        private readonly Dictionary<string, int> _countPerCoffeeType = new();
        private readonly ICoffeeCountStore _coffeeCountStore;
        private MachineDataItem _previousDataItem;

        public MachineDataProcessor(ICoffeeCountStore coffeeCountStore)
        {
            _coffeeCountStore = coffeeCountStore;
        }
        public void ProcessItems(MachineDataItem[] dataItems)
        {
            _countPerCoffeeType.Clear();
            _previousDataItem = null;

            foreach (var dataItem in dataItems)
            {
                ProcessItem(dataItem);
            }

            SaveCountPerCoffeeType();
        }

        private void ProcessItem(MachineDataItem dataItem)
        {
            if (IsNewData(dataItem))
            {
                if (!_countPerCoffeeType.ContainsKey(dataItem.CoffeeType))
                {
                    _countPerCoffeeType.Add(dataItem.CoffeeType, 1);
                }
                else
                {
                    _countPerCoffeeType[dataItem.CoffeeType]++;
                }

                _previousDataItem = dataItem;
            }
            else
            {

            }
        }

        private bool IsNewData(MachineDataItem dataItem)
        {
            return _previousDataItem == null || _previousDataItem.CreatedAt < dataItem.CreatedAt;
        }

        private void SaveCountPerCoffeeType()
        {
            foreach (var entry in _countPerCoffeeType)
            {
                _coffeeCountStore.Save(new CoffeeCountItem(entry.Key, entry.Value));
                //var line = $"{entry.Key}:{entry.Value}";
                //Console.WriteLine(line);
            }
        }
    }
}
