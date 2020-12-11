using System;
using System.Collections.Generic;

namespace Aoc2020
{
    partial class Day10 : Day
    {
        public Day10() {
            ex1.Sort();
            ex2.Sort();
            input.Sort();
        }

        private List<List<int>> clusterize(List<int> adapters) {
            var clusters = new List<List<int>>();
            var cluster = new List<int>();
            clusters.Add(cluster);

            int joltage = 0;
            cluster.Add(joltage);

            foreach(int adapter in adapters) {
                int diff = adapter - joltage;
                if (diff == 3) {
                    cluster = new List<int>();
                    clusters.Add(cluster);
                }

                cluster.Add(adapter);
                joltage = adapter;
            }

            return clusters;
        }

        public String Answer1() {
            int joltage = 0, diff1count = 0, diff2count = 0, diff3count = 0;
            foreach (int adapter in input) {
                switch(adapter - joltage) {
                    case 1:
                        diff1count++;
                        break;
                    case 2:
                        diff2count++;
                        break;
                    case 3:
                        diff3count++;
                        break;
                }
                joltage = adapter;
            }
            diff3count++;

            return $"{diff1count * diff3count}";
        }

        public String Answer2() {
            var clusters = clusterize(input);
            
            long product = 1;
            foreach(var cluster in clusters) {
                switch(cluster.Count) {
                    case 3:
                        product *= 2;
                        break;
                    case 4:
                        product *= 4;
                        break;
                    case 5:
                        product *= 7;
                        break;
                }
            }

            return $"{product}";
        }
    }
}
