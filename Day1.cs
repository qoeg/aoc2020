using System;

namespace Aoc2020
{
    partial class Day1 : Day
    {
        private int[] find2(int[] numbers) {
            foreach (int num1 in numbers) {
                foreach (int num2 in numbers) {
                    if (num1 + num2 == 2020) {
                        return new int[] {num1, num2};
                    }
                }
            }
            return new int[] {};
        }

        private int[] find3(int[] numbers) {
            foreach (int num1 in numbers) {
                foreach (int num2 in numbers) {
                    foreach (int num3 in numbers) {
                        if (num1 + num2 + num3 == 2020) {
                            return new int[] {num1, num2, num3};
                        }
                    }
                }
            }
            return new int[] {};
        }

        public String Answer1() {
            var numbers = find2(input);
            if (numbers.Length == 0)
                return "";

            return (numbers[0] * numbers[1]).ToString();
        }

        public String Answer2() {
            var numbers = find3(input);
            if (numbers.Length == 0)
                return "";

            return (numbers[0] * numbers[1] * numbers[2]).ToString();
        }
    }
}
