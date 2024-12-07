
using System.Linq;

var lines = File.ReadAllLines("Input/data.txt");

var originalSpan = lines.First().Length;
var rowSpan = originalSpan + 2;

var padCharacter = '*';

List<char> padded = [
    ..Enumerable.Repeat(padCharacter, rowSpan),
    ..lines.SelectMany(l => $"{padCharacter}{l}{padCharacter}"),
    ..Enumerable.Repeat(padCharacter, rowSpan),
];

// make direction map

var dirMap = new Dictionary<Direction, int>
{
    [Direction.Left] = -1,
    [Direction.Right] = 1,
    [Direction.Up] = -rowSpan,
    [Direction.Down] = rowSpan,
};

// setup

var obstacle = '#';

var current = '^';
var guardPos = padded.IndexOf(current);
var direction = Direction.Up;
var visited = new HashSet<int>();

// traverse - part 1

while (current != padCharacter)
{
    visited.Add(guardPos);

    var nextPos = guardPos + dirMap[direction];
    current = padded[nextPos];

    if (current == obstacle)
    {
        direction = (Direction)(((int)direction + 1) % 4);
        continue;
    };

    guardPos = nextPos;
}

// result

Console.WriteLine($"Visited: {visited.Count}");

// part 2

// setup 

var p2_startpos = padded.IndexOf('^');
var p2_loopCount = 0;

var withoutStart = visited.ToArray()[1..(visited.Count - 1)];

// taraverse

foreach (var location in withoutStart)
{
    var p2_guardPos = p2_startpos;
    var p2_current = '^';
    var p2_direction = Direction.Up;
    var started = false;
    var p2_visied = new HashSet<string>{
        $"{p2_guardPos},{p2_direction}",
    };

    List<char> newMap = [..padded];
    newMap[location] = 'O';

    while (true)
    {
        var nextPos = p2_guardPos + dirMap[p2_direction];
        p2_current = newMap[nextPos];

        if (p2_current == obstacle || p2_current == 'O')
        {
            p2_direction = (Direction)(((int)p2_direction + 1) % 4);
            continue;
        }

        var key = $"{p2_guardPos},{p2_direction}";
        if (p2_visied.Contains(key) && started)
        {
            p2_loopCount++;
            break;
        }

        if (p2_current == padCharacter)
        {
            break;
        }

        p2_visied.Add(key);
        p2_guardPos = nextPos;
        started = true;
    }

}

// result

Console.WriteLine($"Loops: {p2_loopCount}");


enum Direction { Up = 0, Right = 1, Down = 2, Left = 3 }