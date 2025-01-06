using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Data
{
    public interface ICoffeeCountStore
    {
        void Save(CoffeeCountItem coffeeCountItem);
    }

    public class ConsoleCoffeeCountStore : ICoffeeCountStore
    {
        private readonly TextWriter _textWriter;

        public ConsoleCoffeeCountStore() :this(Console.Out) 
        {
            
        }
        public ConsoleCoffeeCountStore(TextWriter textWriter)
        {
            _textWriter= textWriter;
        }
        public void Save(CoffeeCountItem coffeeCountItem)
        {
            
            var line = $"{coffeeCountItem.CoffeeType}:{coffeeCountItem.Count}";
            //Console.WriteLine(line);
            _textWriter.WriteLine(line);
        }
    }
}
