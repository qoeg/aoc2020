using System;
using System.Collections.Generic;

namespace Aoc2020
{
    partial class Day3 : Day
    {
        List<List<bool>> grid = null;

        public Day3() {
            grid = parse(input);
        }

        private List<List<bool>> parse(string map) {
            var result = new List<List<bool>>();
            result.Add(new List<bool>());

            int x = 0, y = 0;
            foreach (char square in map.ToCharArray()) {
                if (square == '\r')
                    continue;
                
                if (x == 0 && square == '\n')
                    continue;

                if (square == '\n') {
                    result.Add(new List<bool>());
                    y++;
                    x = 0;
                    continue;
                }

                result[y].Add(square == '#');
                x++;
            }

            return result;
        }

        private int countTrees(int slopeX, int slopeY) {
            int treesHit = 0;
            int x = 0, y = 0;
            while (y < grid.Count - 1) {
                x += slopeX;
                y += slopeY;
                if (grid[y][x%grid[y].Count]) {
                    treesHit++;
                }
            }
            return treesHit;
        }

        public String Answer1() {
            return countTrees(3, 1).ToString();
        }

        public String Answer2() {
            int slope1 = countTrees(1, 1);
            int slope2 = countTrees(3, 1);
            int slope3 = countTrees(5, 1);
            int slope4 = countTrees(7, 1);
            int slope5 = countTrees(1, 2);

            return (slope1 * slope2 * slope3 * slope4 * slope5).ToString();
        }
    }
}
