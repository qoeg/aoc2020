using System;
using System.Collections.Generic;

namespace Aoc2020
{
    partial class Day5 : Day
    {
        List<int> ids = null;

        public Day5() {
            ids = getIds(input);
        }

        private Tuple<int, int> locate(char code, Tuple<int, int> range) {
            var lower = range.Item1;
            var upper = range.Item2;
            var count = upper - lower + 1;
            switch (code) {
                case 'F':
                case 'L':
                    upper -= count/2;
                    break;
                case 'B':
                case 'R':
                    lower += count/2;
                    break;
            }
            return new Tuple<int, int>(lower, upper);
        }

        private List<int> getIds(string[] passes) {
            var ids = new List<int>();
            foreach (var pass in passes) {
                var rows = new Tuple<int, int>(0, 127);
                foreach (var code in pass.Substring(0, 7).ToCharArray()) {
                    rows = locate(code, rows);
                }

                var columns = new Tuple<int, int>(0, 7);
                foreach (var code in pass.Substring(7, 3).ToCharArray()) {
                    columns = locate(code, columns);
                }

                ids.Add((rows.Item1 * 8) + columns.Item1);
            }
            ids.Sort();
            return ids;
        }

        public String Answer1() {
            return $"{ids[ids.Count-1]}";
        }

        public String Answer2() {
            int mySeatId = 0, last = ids[0];
            for (int i = 1; i < ids.Count; i++) {
                if (ids[i] != last+1) {
                    mySeatId = last+1;
                    break;
                }
                last = ids[i];
            }

            return $"{mySeatId}";
        }
    }
}
