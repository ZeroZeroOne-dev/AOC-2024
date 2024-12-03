using System.Text.RegularExpressions;

internal partial class Program
{
    [GeneratedRegex(@"mul\(([0-9]+),([0-9]+)\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"mul\(([0-9]+),([0-9]+)\)|do\(\)|don't\(\)")]
    private static partial Regex FilteredRegex();

    private static readonly string Do = "do()";
    private static readonly string Dont = "don't()";

    private static void Main(string[] args)
    {

        var toTest = File.ReadAllText("Input/data.txt");

        // part 1

        var matches = MulRegex().Matches(toTest);

        var totalMul = matches.Aggregate(0, (current, match) => current + int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

        Console.WriteLine($"totalMul: {totalMul}");

        // part 2

        var mulsAndFilters = FilteredRegex().Matches(toTest);

        var doMuls = true;
        var filteredTotal = 0;

        foreach (Match match in mulsAndFilters)
        {
            if (match.Groups[0].Value == Do)
            {
                doMuls = true;
            }
            else if (match.Groups[0].Value == Dont)
            {
                doMuls = false;
            }
            else if(doMuls) {
                filteredTotal += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }

        Console.WriteLine($"filteredTotal: {filteredTotal}");
    }
}