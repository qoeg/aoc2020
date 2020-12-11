using System;
using System.Collections.Generic;
using CommandLine;

namespace Aoc2020
{
    interface Day {
        String Answer1();
        String Answer2();
    }

    public class Options
    {
        [Option('d', "day", Required = false, HelpText = "Day to run.")]
        public int Day { get; set; }
    }

    class Program
    {
        static Dictionary<int, Day> days = new Dictionary<int, Day>() {
            {1, new Day1()},
            {2, new Day2()},
            {3, new Day3()},
            {4, new Day4()},
            {5, new Day5()},
            {6, new Day6()},
            {7, new Day7()},
            {8, new Day8()},
            {9, new Day9()},
            {10, new Day10()},
        };

        static void Main(string[] args)
        {
            bool runAll = true;
            var runDays = new List<int>() {};

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.Day > 0)
                    {
                        runAll = false;
                        runDays = new List<int> {o.Day};
                    }
                });

            foreach(var pair in days) {
                if (!runAll && !runDays.Contains(pair.Key))
                    continue;

                Console.WriteLine($"----- Day {pair.Key} -----");
                Console.WriteLine($"Answer 1: {pair.Value.Answer1()}");
                Console.WriteLine($"Answer 2: {pair.Value.Answer2()}");
            }
        }
    }
}
