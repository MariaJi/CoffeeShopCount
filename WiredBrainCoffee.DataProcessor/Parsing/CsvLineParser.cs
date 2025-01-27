﻿using System.Globalization;
using WiredBrainCoffee.DataProcessor.Model;

namespace WiredBrainCoffee.DataProcessor.Parsing
{
    public class CsvLineParser
    {
        public static MachineDataItem[] Parse(string[] csvlines)
        {
            var machineDataItems = new List<MachineDataItem>();

            foreach (var csvLine in csvlines)
            {
                if(string.IsNullOrWhiteSpace(csvLine))
                {
                    continue;
                }
                var machineDataItem = Parse(csvLine);

                machineDataItems.Add(machineDataItem);
            }

            return machineDataItems.ToArray();
        }

        private static MachineDataItem Parse(string csvLine)
        {
            var lineItems = csvLine.Split(';');
            if (lineItems.Length != 2)
            {
                throw new Exception($"Invlid line: {csvLine}");
            }
            else if(lineItems.Length == 2)
            {
                try
                {
                    var d = DateTime.Parse(lineItems[1], CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    throw new Exception($"Invlid line datetime: {csvLine}");
                    //throw new Exception($"Invlid line: {csvLine}");
                }
            }
            return new MachineDataItem(lineItems[0], DateTime.Parse(lineItems[1], CultureInfo.InvariantCulture));
        }
    }
}
