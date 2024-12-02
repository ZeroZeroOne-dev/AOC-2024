var levels = File.ReadAllLines("Input/data.txt").Select(strReport => strReport.Split(' ').Select(level => int.Parse(level)).ToArray());

var total = levels.Count();
var unSafeLevels = levels.Where(IsNotSave);
var unsafeCount = unSafeLevels.Count();
var saveCount = total - unsafeCount;

var oneStepFromSaveLevels = unSafeLevels.Where(IsOneStepFromSave);
var totalSave = saveCount + oneStepFromSaveLevels.Count();

Console.WriteLine($"Safe: {totalSave}");

static bool IsNotSave(int[] level)
{
    var diffences = new List<int>();

    for (int i = 0; i < level.Length - 1; i++)
    {
        diffences.Add(level[i + 1] - level[i]);
    }

    return diffences.Select(Math.Abs).Any(d => d < 1 || d > 3)
        || (diffences.Any(d => d < 0) && diffences.Any(d => d > 0));
}

static bool IsOneStepFromSave(int[] level)
{
    for (int i = 0; i < level.Length; i++)
    {
        var toTest = level[..i].Concat(level[(i + 1)..]).ToArray();

        if (!IsNotSave(toTest)) return true;
    }

    return false;
}