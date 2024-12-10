internal class Program
{
    public static string[] lines = File.ReadAllLines("Input/data.txt");
    public static int originalSpan = lines.First().Length;
    public static int rowSpan = originalSpan + 2;

    public static char padChar = '*';

    public static List<char> padded = [
        ..Enumerable.Repeat(padChar, rowSpan),
        ..lines.SelectMany(l => $"{padChar}{l}{padChar}"),
        ..Enumerable.Repeat(padChar, rowSpan),
    ];

    // up, right, down, left
    public static int[] D = [-rowSpan, +1, +rowSpan, -1];

    private static void Main(string[] args)
    {
        var zero_indexes = padded
                    .Select((value, index) => new { value, index })
                    .Where(x => x.value == '0')
                    .Select(x => x.index)
                    .ToList();

        var totalScore = zero_indexes.Aggregate(0, (sum, i) => sum + Test(i));    

        Console.WriteLine($"Total Score: {totalScore}");

        var totalScoreP2 = zero_indexes.Aggregate(0, (sum, i) => sum + TestP2(i));

        Console.WriteLine($"Total Score P2: {totalScoreP2}");
    }

    private static int Test(int index, List<int>? reached = null)
    {
        var value = padded[index];
        reached ??= [];

        if (value == '9' && !reached.Contains(index))
        {
            reached.Add(index);
            return 1;
        }

        if(value == padChar) return 0;

        var result = 0;
        foreach (var dir in D)
        {
            var nextIndex = index + dir;
            var nextValue = padded[nextIndex];
            if (nextValue == (char)(value + 1))
            {
                result += Test(nextIndex, reached);
            }
        }

        return result;
    }

    private static int TestP2(int index)
    {
        var value = padded[index];

        if (value == '9')
        {
            return 1;
        }

        if(value == padChar) return 0;

        var result = 0;
        foreach (var dir in D)
        {
            var nextIndex = index + dir;
            var nextValue = padded[nextIndex];
            if (nextValue == (char)(value + 1))
            {
                result += TestP2(nextIndex);
            }
        }

        return result;
    }
}

