using System.Data;
using System.Runtime;

namespace day_7;

using Operation = Func<long, long, long>;

internal class Program
{
    static readonly Operation[] possibleOperations = [Add, Mul, Con];

    private static void Main(string[] args)
    {
        var calibrations = File.ReadAllLines("Input/data.txt")
            .Select(line => line.Split(':'))
            .Select(c => (
                target: long.Parse(c[0]),
                terms: c[1].Trim().Split(' ').Select(long.Parse).ToArray()
            ));

        long total = 0;

        foreach (var (target, terms) in calibrations)
        {
            var possibles = GenerateCombinations(terms.Length - 1);

            foreach (var possible in possibles)
            {
                List<long> progress = [.. terms];

                foreach (var action in possible)
                {
                    var result = action(progress[0], progress[1]);

                    progress.RemoveAt(0);
                    progress.RemoveAt(0);
                    progress.Insert(0, result);
                }

                if (progress[0] == target)
                {
                    total += target;
                    break;
                }
            }
        }

        Console.WriteLine($"Total: {total}");

    }


    static List<List<Operation>> GenerateCombinations(int length, List<Operation>? current = null, List<List<Operation>>? result = null)
    {
        current ??= [];
        result ??= [];

        if (current.Count == length)
        {
            result.Add([.. current]); // Add a copy of the current combination
            return result;
        }

        for (int i = 0; i < possibleOperations.Length; i++)
        {
            current.Add(possibleOperations[i]);
            GenerateCombinations(length, current, result); // Recursive call
            current.RemoveAt(current.Count - 1); // Backtrack
        }

        return result;
    }

    static long Add(long a, long b) => a + b;
    static long Mul(long a, long b) => a * b;
    static long Con(long a, long b) => long.Parse($"{a}{b}");
}