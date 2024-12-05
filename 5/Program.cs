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

var incorrects = updates.Where(u => !isCorrect(u));

var reordered = incorrects.Select(order);

var totalReordered = reordered.Aggregate(0, (acc, update) => acc + int.Parse(update[update.Length / 2]));

Console.WriteLine($"Reordered: {totalReordered}");

// functions

bool isCorrect(string[] update)
{
    var passes = true;

    for (var i = 0; i < update.Length; i++)
    {
        var page = update[i];
        if (!ruleMap.TryGetValue(page, out var ruleSet))
        {
            continue;
        }

        var before = update[..i];
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

    var reoredred = new List<string>();

    for (var i = 0; i < update.Length; i++)
    {
        var page = update[i];

        if (!ruleMap.TryGetValue(page, out var ruleSet))
        {
            reoredred.Add(page);
            continue;
        }

        var firstAfter = reoredred.FirstOrDefault(r => ruleSet.Contains(r));

        if (firstAfter == null)
        {
            reoredred.Add(page);
            continue;
        }

        var afterIndex = reoredred.IndexOf(firstAfter);
        reoredred.Insert(afterIndex, page);
    }

    return [.. reoredred];
}



