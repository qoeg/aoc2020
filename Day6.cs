using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    partial class Day6 : Day
    {
        private List<List<string>> groups = null;

        public Day6() {
            groups = Utils.Parse.LineGroups(input);
        }

        public String Answer1() {
            var counts = new List<int>();
            foreach (var group in groups) {
                var answers = new Dictionary<char, bool>();
                foreach (var person in group) {
                    foreach (var answer in person.ToCharArray()) {
                        answers.TryAdd(answer, true);
                    }
                }
                counts.Add(answers.Count);
            }

            return $"{counts.Sum()}";
        }

        public String Answer2() {
            var counts = new List<int>();
            foreach (var group in groups) {
                var answers = new Dictionary<char, bool>();
                foreach (var answer in group[0].ToCharArray()) {
                    answers.Add(answer, true);
                }

                for (int i = 1; i < group.Count; i++) {
                    foreach (var answer in answers) {
                        if (!group[i].Contains(answer.Key)) {
                            answers[answer.Key] = false;
                        }
                    }
                }

                counts.Add(answers.Where(a => a.Value == true).Count());
            }

            return $"{counts.Sum()}";
        }
    }
}
