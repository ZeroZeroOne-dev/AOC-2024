
var lines = File.ReadAllLines("Input/data.txt");

var left = new List<int>();
var right = new List<int>();

foreach (var line in lines)
{
    var split = line.Split("   ");
    left.Add(int.Parse(split[0]));
    right.Add(int.Parse(split[1]));
}

left.Sort();
right.Sort();

var dataLength = lines.Length;

// part-1

var distance = 0;
for (int i = 0; i < dataLength;i++)
{
    distance += Math.Abs(left[i] - right[i]);
}

Console.WriteLine($"Distance: {distance}");

// part-2

var similarity = 0;
for (int i = 0; i < dataLength;i++)
{
    var current = left[i];
    similarity += current * right.Count(c => c == current);
}

Console.WriteLine($"Similarity: {similarity}");