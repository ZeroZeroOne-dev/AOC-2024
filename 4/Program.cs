// inspired by https://stackoverflow.com/a/2208524


// read matrix
var lines = File.ReadAllLines("Input/data.txt").Select(l => l.ToList()).ToList();

// insert * character to indicate boundaries

var paddingChar = '*';

var originalSpan = lines.First().Count;

lines.Insert(0, new string(paddingChar, originalSpan + 2).ToList());
lines.Add(new string(paddingChar, originalSpan + 2).ToList());

lines.ForEach(l =>
{
    l.Insert(0, paddingChar);
    l.Add(paddingChar);
});

#region PART 1

// calculate vectors

var rowSpan = originalSpan + 2;

var oneDimensional = lines.SelectMany(l => l).ToArray();

int[] D = [-rowSpan, -1, 1, rowSpan, -rowSpan - 1, -rowSpan + 1, rowSpan - 1, rowSpan + 1];


// do search

var target = "XMAS";
var total = 0;

for (int i = 0; i < oneDimensional.Length; i++)
{
    if (oneDimensional[i] == paddingChar) continue;

    foreach (var dir in D)
    {
        var found = true;

        for (int j = 0; j < target.Length; j++)
        {
            var currentIndex = i + j * dir;

            if (oneDimensional[currentIndex] != target[j])
            {
                found = false;
                break;
            }
        }

        if (found) total++;
    }
}

Console.WriteLine($"P1 Total: {total}");

#endregion

#region PART 2

// calculate vectors

int[] p2_D = [-rowSpan - 1, -rowSpan + 1, rowSpan - 1, rowSpan + 1];

var p2_target = "MAS";
var diagAs = new List<int>();

// do search

for (int i = 0; i < oneDimensional.Length; i++)
{
    if (oneDimensional[i] == paddingChar) continue;

    foreach (var dir in p2_D)
    {
        var found = true;
        var aIndex = 0;

        for (int j = 0; j < p2_target.Length; j++)
        {
            var currentIndex = i + j * dir;
            var current = oneDimensional[currentIndex];

            if (current != p2_target[j])
            {
                found = false;
                break;
            }

            if (current == 'A')
            {
                aIndex = currentIndex;
            }
        }

        if (found)
        {
            diagAs.Add(aIndex);
        }
    }
}

var count = diagAs
            .GroupBy(x => x)              
            .Where(g => g.Count() == 2)   
            .Count(); 

Console.WriteLine($"P2 Total: {count}");

#endregion
