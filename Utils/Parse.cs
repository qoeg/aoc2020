using System;
using System.Collections.Generic;

namespace Aoc2020.Utils
{
    static class Parse
    {
        public static Grid<char> CharGrid(string doc) {
            var grid = new Grid<char>();
            int x = 0, y = 0;

            int pos = -1;
            while (pos < doc.Length-1) {
                pos++;
                var c = doc[pos];

                if (c == '\r') {
                    continue;
                }

                if (c == '\n') {
                    y++;
                    x = 0;
                    continue;
                }

                var cell = new CharCell {
                    Position = new Coordinate{X = x, Y = y},
                    Value = c,
                };
                grid.Add(cell);
                x++;
            }

            return grid;
        }
        
        public static List<List<string>> LineGroups(string doc) {
            var results = new List<List<string>>();
            var group = new List<string>();
            var line = new List<char>();

            int pos = -1;
            while (pos < doc.Length-1) {
                pos++;
                var cha = doc[pos];

                if (cha == '\r')
                    continue;

                if (cha != '\n') {
                    line.Add(cha);
                    continue;
                }

                if (line.Count != 0) {
                    group.Add(new string(line.ToArray()));
                    line = new List<char>();
                    continue;
                }

                results.Add(group);
                group = new List<string>();
            }

            group.Add(new string(line.ToArray()));
            results.Add(group);
            return results;
        }
    }
}