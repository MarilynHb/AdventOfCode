using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public class CalibrationValues_Day1
{
    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? throw new NullReferenceException();
    const string year = "2023";
    internal void SumOfAllCalibrationValues()
    {
        var path = $"{projectPath}\\{year}\\Input\\{year}_Day1.txt";
        List<int> numbers = [];
        IEnumerable<string> lines = File.ReadAllLines(path, Encoding.UTF8);
        if (!lines.Any())
        {
            Console.WriteLine("Empty File");
            return;
        }

        foreach (string line in lines)
        {
            var number = (from x in line where char.IsDigit(x) select x);
            var final = int.Parse(number.First().ToString() + number.Last().ToString());
            numbers.Add(final);
        }
        Console.WriteLine(numbers.Sum());
    }
}

