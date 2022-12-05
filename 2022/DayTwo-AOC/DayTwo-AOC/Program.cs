// See https://aka.ms/new-console-template for more information
var entryValues = ReadEntryValuesFromFile();
var score = CalculateScoreAccordingToGuide_DayTwo_PartOne(entryValues);
Console.WriteLine($"Day Two - Part One Result : My total score according to the guide is {score}");

List<(int opponentPlay, int myPlay)> ReadEntryValuesFromFile()
{
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    var result = new List<(int opponentPlay, int myPlay)> ();
    using (var reader = new StreamReader($@"{directory}\data.csv"))
    {
        while (!reader.EndOfStream)
        {
            var value = reader.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var splittedValue = value.Split(' ');
                result.Add((ConvertStringValueToInt(splittedValue[0]), ConvertStringValueToInt(splittedValue[1])));
            }
        }
    }
    return result;
}

int ConvertStringValueToInt(string value)
{
    switch(value)
    {
        case "A" : return 1;
        case "X": return 1;

        case "B" : return 2;
        case "Y": return 2;

        case "C" : return 3;
        case "Z": return 3;

        default : return 0;
    }
}

int GetScoreForEachRound(int opponentPlay, int myPlay)
{
    var winCondition = (myPlay == 1 && opponentPlay == 3) || (opponentPlay < myPlay && (myPlay != 3 || opponentPlay != 1));
    var win = 0;
    if (opponentPlay == myPlay)
        win = 3;
    else if (winCondition)
        win = 6;
    return win + myPlay;
}

int CalculateScoreAccordingToGuide_DayTwo_PartOne(List<(int opponentPlay, int myPlay)> guide)
{
    return guide.Sum(x => GetScoreForEachRound(x.opponentPlay, x.myPlay));
}