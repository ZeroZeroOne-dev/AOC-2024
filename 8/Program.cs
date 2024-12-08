// make one dimensional array
// use dif of a and b to calculate new coordinate

var lines = File.ReadAllLines("Input/data.txt");

var originalSpan = lines.First().Length;
var rowSpan = originalSpan;

List<char> padded = [
    ..lines.SelectMany(l => l),
];

var index1 = padded.IndexOf('a');
var index2 = padded.LastIndexOf('a');

var indexDiff = index2 - index1;

var t1 = index1 - indexDiff;

var t1X = t1 % rowSpan;
var t1Y = t1 / rowSpan;

Console.WriteLine($"t1X: {t1X}, t1Y: {t1Y}");

var t2 = index2 + indexDiff;

var t2X = t2 % rowSpan;
var t2Y = t2 / rowSpan;

Console.WriteLine($"t2X: {t2X}, t2Y: {t2Y}");