// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

var entryValue = ReadEntryValuesFromFile();
var number = GetNumberOfAssignmentsFullyContainedInAnother_DayFour_PartOne(entryValue);
Console.WriteLine($"Day Three - Part One Result : The number of assignement pairs where one range fully contains the other is : {number}");
Console.WriteLine("");
List<string> ReadEntryValuesFromFile()
{
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    var result = new List<string>();
    using (var reader = new StreamReader($@"{directory}\data.csv"))
    {
        while (!reader.EndOfStream)
        {
            var value = reader.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                result.Add(value);
            }
        }
    }
    return result;
}

int GetNumberOfAssignmentsFullyContainedInAnother_DayFour_PartOne(List<string> entryValues)
{
    var entryInNumbers = ConvertEntryToNumber(entryValues);
    var result = 0;
    foreach ( var entry in entryInNumbers )
    {
        if (IsOneAssignementSubsetOfAnother(entry.ElementAt(0), entry.ElementAt(1)))
            result++;
    }
    return result;
}

List<List<HashSet<int>>> ConvertEntryToNumber(List<string> entry)
{
    var result = new List<List<HashSet<int>>>();
    foreach(var pair in entry)
    {
        var pairNumbers = new List<HashSet<int>>();
        var limits = pair.Split(',');
        foreach(var limit in limits)
        {
            var assignements = new HashSet<int>();
            var limitNumber = limit.Split('-');
            var lowerLimit = Int32.Parse(limitNumber[0]);
            var upperLimit = Int32.Parse(limitNumber[1]);
            while (lowerLimit <= upperLimit)
            {
                assignements.Add(lowerLimit++);
            }
            pairNumbers.Add(assignements);
        }
        result.Add(pairNumbers);
    }
    return result;
}

bool IsOneAssignementSubsetOfAnother(HashSet<int> assignment1, HashSet<int> assignment2)
{
    return assignment1.IsSubsetOf(assignment2) || assignment2.IsSubsetOf(assignment1);
}