using System;
using System.Linq;

namespace Aoc2020
{
    partial class Day9 : Day
    {
        private long invalidValue;

        private Tuple<int, int> check(int index, int preamble) {
            for (int j = (index - preamble); j < index; j++) {
                for (int k = (index - preamble); k < index; k++) {
                    if (j == k) {
                        continue;
                    }
                    
                    if (input[index] == input[j] + input[k]) {
                        return new Tuple<int, int>(j, k);
                    }
                }
            }

            return null;
        }

        private long[] findSet() {
            for (int i = 0; i < input.Length; i++) {
                long sum = 0; int length = 0;
                for (int j = i; j < input.Length; j++) {
                    length++;
                    sum += input[j];
                    if (sum == invalidValue && length > 1) {
                        return input.Skip(i).Take(length).ToArray();
                    }
                    if (sum > invalidValue) {
                        break;
                    }
                }
            }
            return new long[] {};
        }

        public String Answer1() {
            int preamble = 25;
            for (int i = preamble; i < input.Length; i++) {
                if (check(i, 25) == null) {
                    invalidValue = input[i];
                    break;
                }
            }

            return $"{invalidValue}";
        }

        public String Answer2() {
            var set = findSet();
            var min = set.Min();
            var max = set.Max();

            return $"{min + max}";
        }
    }
}
