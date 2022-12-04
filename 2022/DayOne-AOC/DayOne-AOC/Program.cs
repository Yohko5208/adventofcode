// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

var entryValues = ReadEntryValuesFromFile();
var dayOne_PartOne = CalculateMaxcaloriesCarriedByElf_DayOne_PartOne(entryValues);
Console.WriteLine($"Day One - Part One Result : The most amount of calories ({dayOne_PartOne.amount}) is carried by elf {dayOne_PartOne.elf+1}");

var dayOne_PartTwo = CalculateMaxcaloriesCarriedByToThreeElf_DayOne_PartTwo(entryValues);
Console.WriteLine("");
Console.WriteLine("Day One - Part Two Result : The elves who are top three in calorie amounts are : ");
foreach((int amount, int elf) elf in dayOne_PartTwo)
    Console.WriteLine($"     ({elf.amount}) calories carried by elf {elf.elf + 1}");
Console.WriteLine($"The sum of calories carried by the three is :  {dayOne_PartTwo.Sum(x=> x.amount)}");

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

(int amount, int elf) CalculateMaxcaloriesCarriedByElf_DayOne_PartOne(Dictionary <int, List<int>> calorieList)
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
    return (elf, maxCalorie);
}

List<(int amount, int elf)> CalculateMaxcaloriesCarriedByToThreeElf_DayOne_PartTwo(Dictionary<int, List<int>> calorieList)
{
    var sortedList = from entry in calorieList orderby entry.Value.Sum() descending select (entry.Value.Sum(), entry.Key);
    return sortedList.Take(3).ToList();
}

