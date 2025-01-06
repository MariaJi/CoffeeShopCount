using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessorTests.Data
{
    
    public class ConsoleCoffeeCountStoreTests
    {
        [Fact]
        public void ShouldWriteOutputToConsoleTest()
        {
            //Arrange
            var coffeItem = new CoffeeCountItem("Cappuccino", 5);
            var stringWriter = new StringWriter();
            var consoleCofferStore = new ConsoleCoffeeCountStore(stringWriter);

            //Act
            consoleCofferStore.Save(coffeItem);

            //Asert
            var result = stringWriter.ToString();
            Assert.Equal($"{coffeItem.CoffeeType}:{coffeItem.Count}{Environment.NewLine}", result);
        }
    }
}
