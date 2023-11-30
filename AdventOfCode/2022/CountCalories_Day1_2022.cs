using System.Text;

namespace AdventOfCode;

public class CountCalories_Day1_2022
{
    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? throw new NullReferenceException();
    const string year = "2022";
    internal void TopElfWithMaxTotalCalories(int take)
    {
        var path = $"{projectPath}\\{year}\\Input\\{year}_Day1.txt";

        IEnumerable<string> lines = File.ReadAllLines(path, Encoding.UTF8);
        if (!lines.Any()) 
        {
            Console.WriteLine("Empty File");
            return;
        }

        int id = 0;
        IList<Elf> elves = [new() { Id = id }];
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                ++id;
                elves.Add(new Elf { Id = id });
                continue;
            }
            var elf = elves[id].TotalCalories += long.Parse(line);
        }
        var top = (from x in elves
                   orderby x.TotalCalories descending
                   select x.TotalCalories)
                  .Take(take)
                  .ToArray();

        Console.WriteLine(top.Sum());
    }

    public class Elf
    {
        public int Id { get; set; }
        public long TotalCalories { get; set; }
    }
}