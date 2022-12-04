// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

var entryValues = ReadEntryValuesFromFile();
var result = CalculateMaxcaloriesCarriedByElf_DayOne(entryValues);
Console.WriteLine($"The most amount of calories ({result.amount}) is carried by elf {result.elf+1}");

Dictionary<int, List<int>> ReadEntryValuesFromFile()
{
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    var result = new Dictionary<int, List<int>>();
    using (var reader = new StreamReader($@"{directory}\data.csv"))
    {
        List<int> calorieListOfAnElf = new List<int>();
        int i = 0;
        while (!reader.EndOfStream)
        {
            var value = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(value))
            {
                result.Add(i, calorieListOfAnElf);
                calorieListOfAnElf = new List<int>();
                i++;
            }
            else
            {
                calorieListOfAnElf.Add(Int32.Parse(value));
            }
        }
    }
    return result;
}

(int amount, int elf) CalculateMaxcaloriesCarriedByElf_DayOne(Dictionary <int, List<int>> calorieList)
{
    int maxCalorie = 0;
    int elf = 0;
    foreach(var elfCalorie in calorieList)
    {
        var sum = elfCalorie.Value.Sum();
        if (sum > maxCalorie)
        {
            maxCalorie = sum;
            elf = elfCalorie.Key;
        }
    }
    result.elf = elf;
    result.amount = maxCalorie;
    return result;
}