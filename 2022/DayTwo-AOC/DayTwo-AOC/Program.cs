// See https://aka.ms/new-console-template for more information
var entryValues = ReadEntryValuesFromFile();
var score = CalculateScoreAccordingToGuide_DayTwo_PartOne(entryValues);
Console.WriteLine($"Day Two - Part One Result : My total score according to the guide is {score}");
Console.WriteLine("");
var scoreTwo = CalculateScoreAccordingToGuide_DayTwo_PartTwo(entryValues);
Console.WriteLine($"Day Two - Part Two Result : My total score according to the guide should be {scoreTwo}");

List<(string , string)> ReadEntryValuesFromFile()
{
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    var result = new List<(string, string)> ();
    using (var reader = new StreamReader($@"{directory}\data.csv"))
    {
        while (!reader.EndOfStream)
        {
            var value = reader.ReadLine();
            if (!string.IsNullOrWhiteSpace(value))
            {
                var splittedValue = value.Split(' ');
                result.Add((splittedValue[0], splittedValue[1]));
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

int CalculateScoreAccordingToGuide_DayTwo_PartOne(List<(string opponentPlay, string myPlay)> guide)
{
    return guide.Sum(x => GetScoreForEachRound(ConvertStringValueToInt(x.opponentPlay), ConvertStringValueToInt(x.myPlay)));
}

(int opponentPlay, int myPlay) ConvertOutcomeToGuide((string opponentPlay, string desiredOutcome) value)
{
    var opponent = 0;
    switch (value.opponentPlay)
    {
        case "A": opponent = 1; break;

        case "B": opponent = 2; break;

        case "C": opponent = 3; break;

        default: opponent = 0; break;
    }
    var me = 0;
    switch (value.desiredOutcome)
    {
        case "X":
            if (opponent == 1)
                me = 3;
            else
                me = opponent - 1;
            break;

        case "Y": 
            me = opponent; 
            break;

        case "Z":
            if (opponent == 3)
                me = 1;
            else
                me = opponent + 1;
            break;

        default: 
            opponent = 0; 
            break;
    }

    return (opponent, me);
}

int CalculateScoreAccordingToGuide_DayTwo_PartTwo(List<(string opponentPlay, string desiredOutcome)> guide)
{
    return guide.Sum(x =>
    {
        var (opponentPlay, myPlay) = ConvertOutcomeToGuide((x.opponentPlay, x.desiredOutcome));
        return GetScoreForEachRound(opponentPlay, myPlay);
        }
    );
}