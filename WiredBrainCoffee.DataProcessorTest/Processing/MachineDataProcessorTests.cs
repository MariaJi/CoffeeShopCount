using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;
using WiredBrainCoffee.DataProcessor.Processing;

namespace WiredBrainCoffee.DataProcessorTests.Processing
{
    public class MachineDataProcessorTests :IDisposable
    {
        public readonly FakeCoffeeCountStore _coffeCountStore ;

        public readonly MachineDataProcessor _machineDataProcessor ;
        public MachineDataProcessorTests()
        {
            _coffeCountStore = new FakeCoffeeCountStore();
            _machineDataProcessor = new MachineDataProcessor(_coffeCountStore);
        }

        
        [Fact]
        public void ShouldMachineDataProcessorTest()
        {
            //var coffeCountStore = new FakeCoffeeCountStore();
            //arrange
            //var machineDataProcessor = new MachineDataProcessor(coffeCountStore);
            var items = new[]
            {
                new MachineDataItem("Espresso", new DateTime(2022,10, 27, 8,01,16)),
                new MachineDataItem("Espresso", new DateTime(2022,11, 27, 8,01,16)),
                new MachineDataItem("Cappuccino", new DateTime(2022,12, 27, 8,01,16)),

            };
            // Act
            _machineDataProcessor.ProcessItems(items);
            var count = _coffeCountStore.SavedItems.Count();
            Assert.Equal(2, _coffeCountStore.SavedItems.Count());
            var item = _coffeCountStore.SavedItems[0];
            Assert.Equal("Espresso", item.CoffeeType);



            item = _coffeCountStore.SavedItems[1];
            Assert.Equal(1, item.Count);
            Assert.Equal("Cappuccino", item.CoffeeType);


        }


        [Fact]
        public void ShouldIgnoreNotNewerDataTest()
        {
            //var coffeCountStore = new FakeCoffeeCountStore();
            //arrange
            //var machineDataProcessor = new MachineDataProcessor(coffeCountStore);
            var items = new[]
            {
                new MachineDataItem("Espresso", new DateTime(2022,10, 27, 8,01,16)),
                new MachineDataItem("Espresso", new DateTime(2022,11, 27, 8,01,16)),
                new MachineDataItem("Espresso", new DateTime(2022,11, 27, 7,01,16)),
                new MachineDataItem("Cappuccino", new DateTime(2022,12, 27, 8,01,16)),
                new MachineDataItem("Cappuccino", new DateTime(2022,12, 27, 8,01,16)),
                new MachineDataItem("Cappuccino", new DateTime(2022,11, 27, 8,01,16)),


            };
            // Act
            _machineDataProcessor.ProcessItems(items);
            var count = _coffeCountStore.SavedItems.Count();
            Assert.Equal(2, _coffeCountStore.SavedItems.Count());
            var item = _coffeCountStore.SavedItems[0];
            Assert.Equal("Espresso", item.CoffeeType);



            item = _coffeCountStore.SavedItems[1];
            Assert.Equal(1, item.Count);
            Assert.Equal("Cappuccino", item.CoffeeType);


        }

        [Fact]
        public void ShouldClearPreviousCoffeeCountTest()
        {
            //var coffeCountStore = new FakeCoffeeCountStore();
            ////arrange
            //var machineDataProcessor = new MachineDataProcessor(coffeCountStore);
            var items = new[]
            {
                new MachineDataItem("Espresso", new DateTime(2022,10, 27, 8,01,16)),
                new MachineDataItem("Espresso", new DateTime(2022,11, 27, 8,01,16)),
                new MachineDataItem("Cappuccino", new DateTime(2022,12, 27, 8,01,16)),

            };
            // Act
            _machineDataProcessor.ProcessItems(items);

            _machineDataProcessor.ProcessItems(items);
            var count = _coffeCountStore.SavedItems.Count();
            Assert.Equal(4, _coffeCountStore.SavedItems.Count());

            var item = _coffeCountStore.SavedItems[0];
            Assert.Equal("Espresso", item.CoffeeType);
            Assert.Equal(2, item.Count);
            item = _coffeCountStore.SavedItems[1];
            Assert.Equal(1, item.Count);
            Assert.Equal("Cappuccino", item.CoffeeType);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            // run after every test
        }
    }

    public class FakeCoffeeCountStore : ICoffeeCountStore
    {
        public List<CoffeeCountItem> SavedItems { get; } = new();
        public void Save(CoffeeCountItem coffeeCountItem)
        {
            SavedItems.Add(coffeeCountItem);
           
        }
    }
}  
