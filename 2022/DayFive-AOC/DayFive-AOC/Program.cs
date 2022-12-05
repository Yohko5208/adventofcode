// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

var entryValues = ReadEntryValuesFromFile();
Console.WriteLine("Hello, World!");

(List<string> initialState, List<string> instructions) ReadEntryValuesFromFile()
{
    var directory = AppDomain.CurrentDomain.BaseDirectory;
    var initialState = new List<string>();
    var instructions = new List<string>();
    bool isInitialstate = true;
    using (var reader = new StreamReader($@"{directory}\data.csv"))
    {
        while (!reader.EndOfStream)
        {
            var value = reader.ReadLine();

            if(string.IsNullOrEmpty(value))
            {
                isInitialstate = false;
            }
            else
            {
                if (isInitialstate)
                {
                    initialState.Add(value);
                }
                else
                {
                    instructions.Add(value);

                }
            }
        }
    }
    return (initialState, instructions);
}