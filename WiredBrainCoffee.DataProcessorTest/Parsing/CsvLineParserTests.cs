using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiredBrainCoffee.DataProcessor.Parsing;

namespace WiredBrainCoffee.DataProcessorTests.Parsing;

public class CsvLineParserTests
{
    [Fact]
    public void ShouldParseVaildLine()
    {
        //Arange
        string[] cvsLines = new string[] { "Espresso; 10 / 27 / 2022 8:01:16 AM" };
        //string[] cvsLines = new string[] { "" };
        //Act method needs to be test
        var result = CsvLineParser.Parse(cvsLines);
        //Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Espresso", result[0].CoffeeType);
        Assert.Equal(new DateTime(2022,10, 27, 8,01,16), result[0].CreatedAt);
    }

    [Fact]
    public void ShouldSkipEmptyLine()
    {
        //Arange
        //string[] cvsLines = new string[] { "Espresso; 10 / 27 / 2022 8:01:16 AM" };
        string[] cvsLines = new string[] { "", "    "};
        //Act method needs to be test
        var result = CsvLineParser.Parse(cvsLines);
        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        
    }

    [Fact]
    public void ShouldThrowExceptionForInvalidLine()
    {
        var lineItem = "Espresso";
        //Arange
        string[] cvsLines = new string[] { lineItem };
        //string[] cvsLines = new string[] { "", "    " };
        //Act method needs to be test
        //var result = CsvLineParser.Parse(cvsLines);
        var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(cvsLines));
        //Assert
        Assert.Equal($"Invlid line: {lineItem}", exception.Message);

    }

    [Fact]
    public void ShouldThrowExceptionForInvalidLine2()
    {
        var lineItem = "Espresso; invalidDatetime";
        //Arange
        string[] cvsLines = new string[] { lineItem };
        //string[] cvsLines = new string[] { "", "    " };
        //Act method needs to be test
        //var result = CsvLineParser.Parse(cvsLines);
        var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(cvsLines));
        //Assert
        Assert.Equal($"Invlid line datetime: {lineItem}", exception.Message);

    }

    [InlineData("Espresso; invalidDatetime", "Invlid line datetime:")]
    [InlineData("Espresso", "Invlid line:")]
    [Theory]
    public void ShouldThrowExceptionForInvalidLines(string lineItem, string expectedMessagePrefix)
    {
        //var lineItem = "Espresso; invalidDatetime";
        //Arange
        string[] cvsLines = new string[] { lineItem };
        //string[] cvsLines = new string[] { "", "    " };
        //Act method needs to be test
        //var result = CsvLineParser.Parse(cvsLines);
        var exception = Assert.Throws<Exception>(() => CsvLineParser.Parse(cvsLines));
        //Assert
        Assert.Equal($"{expectedMessagePrefix} {lineItem}", exception.Message);

    }
}
