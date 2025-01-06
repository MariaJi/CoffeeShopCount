using MyNamespace;
using WiredBrainCoffee.DataProcessor.Data;
using WiredBrainCoffee.DataProcessor.Model;
using WiredBrainCoffee.DataProcessor.Parsing;
using WiredBrainCoffee.DataProcessor.Processing;

Console.WriteLine("---------------------------------------");
Console.WriteLine("  Wired Brain Coffee - Data Processor  ");
Console.WriteLine("---------------------------------------");
Console.WriteLine();

const string fileName = "CoffeeMachineData.csv";
string[] csvLines = File.ReadAllLines(fileName);

MachineDataItem[] machineDataItems = CsvLineParser.Parse(csvLines);

var machineDataProcessor = new MachineDataProcessor(new ConsoleCoffeeCountStore());
machineDataProcessor.ProcessItems(machineDataItems);

Console.WriteLine();
Console.WriteLine($"File {fileName} was successfully processed!");

Console.ReadLine();

// await Utilities.ShowConsoleAnimation();

namespace MyNamespace
{
    public static class Utilities
    {
        public static async Task ShowConsoleAnimation()
        {
            string[] animations = ["| -", "/ \\", "- |", "\\ /"];
            for (int i = 0; i < 20; i++)
            {
                foreach (string s in animations)
                {
                    Console.Write(s);
                    await Task.Delay(50);
                    Console.Write("\b\b\b");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}