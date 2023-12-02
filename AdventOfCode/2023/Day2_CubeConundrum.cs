using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;
public class Day2_CubeConundrum
{
    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName ?? throw new NullReferenceException();
    const string year = "2023";
    internal void SumOfPossibleGames()
    {
        var path = $"{projectPath}\\{year}\\Input\\{year}_Day2.txt";
        IEnumerable<string> lines = File.ReadAllLines(path, Encoding.UTF8);
        if (!lines.Any())
        {
            Console.WriteLine("Empty File");
            return;
        }
        List<Game> Games = [];
        int id = 0;
        foreach (var line in lines)
        {
            var game = new Game() { Id = ++id};
            var sets = line.Split(":")[1].Split(";");
            List<GameSet> gameSets = [];
            foreach(var set in sets)
            {
                GameSet gameSet = new();
                var cubes = set.Split(",", StringSplitOptions.TrimEntries);
                foreach(var cube in cubes)
                {
                    var c = cube.Split(" ");
                    var cubesCount = int.Parse(c[0]);
                    switch (c[1])
                    {
                        case "red":
                            if (cubesCount > 12) game.IsImpossible = true;
                            gameSet.RedCount += cubesCount;
                            break;
                        case "green":
                            if (cubesCount > 13) game.IsImpossible = true;
                            gameSet.GreenCount += cubesCount;
                            break;
                        case "blue":
                            if (cubesCount > 14) game.IsImpossible = true;
                            gameSet.BlueCount += cubesCount;
                            break;
                    }
                }
                gameSets.Add(gameSet);
            }
            game.GameSets = gameSets;
            var minRed = (from x in gameSets orderby x.RedCount descending select x.RedCount).First() ;
            var minGreen = (from x in gameSets orderby x.GreenCount descending select x.GreenCount).First();
            var minBlue = (from x in gameSets orderby x.BlueCount descending select x.BlueCount).First();
            game.MinimumSet = new GameSet(minRed, minGreen, minBlue);
            Games.Add(game);
        }
        Console.WriteLine("Part 1: " + Games.Where(t => !t.IsImpossible).Select(t => t.Id).Sum());
        Console.WriteLine("Part 2:" + Games.Select(t => t.MinimumSet.Power).Sum());
    }

}

class Game
{
    public int Id { get; set; }
    public IEnumerable<GameSet> GameSets { get; set; } = [];
    public GameSet MinimumSet { get; set; } = new();
    public bool IsImpossible { get; set; }
}
 class GameSet
{
    public GameSet(int r, int g, int b)
    {
        RedCount = r; GreenCount = g; BlueCount = b;
    }
    public GameSet()
    {
    }
    public int RedCount { get; set; }
    public int GreenCount { get; set;}
    public int BlueCount { get; set;}
    public int Power => RedCount * GreenCount * BlueCount;
}