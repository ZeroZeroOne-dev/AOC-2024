var lines = File.ReadAllLines("Input/data.txt");

var rules = lines.Where(l => l.Contains('|')).Select(l => l.Split('|')).Select(r => (index: r[0], after: r[1]));
var updates = lines.Where(l => l.Contains(',')).Select(l => l.Split(','));

// build rules

Dictionary<string, HashSet<string>> ruleMap = [];

foreach (var (index, after) in rules)
{
    if (ruleMap.TryGetValue(index, out var ruleSet))
    {
        ruleSet.Add(after);
    }
    else
    {
        ruleMap[index] = [after];
    }
}

// part 1

var corrects = updates.Where(isCorrect);
var total = corrects.Aggregate(0, (acc, update) => acc + int.Parse(update[update.Length / 2]));

Console.WriteLine($"Correct: {total}");

// part 2

var incorrects = updates.Except(corrects).ToArray();

var reordered = incorrects.Select(order);

var totalReordered = reordered.Aggregate(0, (acc, update) => acc + int.Parse(update[update.Length / 2]));

Console.WriteLine($"Reordered: {totalReordered}");

// functions

bool isCorrect(IEnumerable<string> update)
{
    var passes = true;
    var updateArr = update.ToArray();

    for (var i = 0; i < updateArr.Length; i++)
    {
        var page = updateArr[i];
        if (!ruleMap.TryGetValue(page, out var ruleSet))
        {
            continue;
        }

        var before = updateArr[..i];
        if (before.Any(ruleSet.Contains))
        {
            passes = false;
            break;
        }
    }

    return passes;
}

string[] order(string[] update)
{
    var remade = new List<string>(update);

    while (!isCorrect(remade))
    {

        for (var i = 0; i < remade.Count; i++)
        {

            var page = remade[i];
            if (!ruleMap.TryGetValue(page, out var ruleSet))
            {
                continue;
            }

            var index = remade.FindIndex(ruleSet.Contains);

            if (index == -1)
            {
                continue;
            }

            remade.RemoveAt(i);
            remade.Insert(index, page);
        }

    }

    return [.. remade];
}



