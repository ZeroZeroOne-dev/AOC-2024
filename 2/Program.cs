var levels = File.ReadAllLines("Input/data.txt").Select(strReport => strReport.Split(' ').Select(level => int.Parse(level)).ToArray());

var safeLevels = levels.Where(IsSave);

var safe = 0;
// foreach (var level in levels)
// {
//     var increase = 0;
//     var decrease = 0;

//     for (int i = 0; i < level.Length; i++)
//     {
//         if (i == level.Length - 1)
//         {
//             safe++;
//             continue;
//         }

//         var difference = level[i] - level[i + 1];
//         if (difference < 0)
//         {
//             increase++;
//         }
//         else if (difference > 0)
//         {
//             decrease++;
//         }
//         else
//         {
//             break;
//         }

//         if (increase != 0 && decrease != 0)
//         {
//             break;
//         }

//         var absDifference = Math.Abs(difference);
//         if (absDifference < 1 || absDifference > 3)
//         {
//             break;
//         }
//     }
// }

foreach (var level in levels)
{

}

static bool IsSave(int[] level)
{
    var pairs = new List<(int first, int second)>();

    for (int i = 0; i < level.Length - 1; i++)
    {
        pairs.Add((level[i], level[i+1]));
    }

    return false;
}

Console.WriteLine($"Safe: {safe}");