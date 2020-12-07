using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    partial class Day7 : Day
    {
        class bag
        {
            public bag(string name) {
                Name = name;
                Children = new Dictionary<bag, int>();
            }
            public string Name {get; set;}
            public Dictionary<bag, int> Children {get; set;}

            public bool Contains(string name) {
                foreach (var child in this.Children) {
                    if (child.Key.Name == name) {
                        return true;
                    }
                    if (child.Key.Contains(name)) {
                        return true;
                    }
                }
                return false;
            }

            public int CountInnerBags() {
                int count = 0;
                foreach (var child in this.Children) {
                    count += child.Value;
                    count += (child.Value * child.Key.CountInnerBags());
                }
                return count;
            }
        }

        private Dictionary<string, bag> bags = null;

        public Day7() {
            bags = getBags(input);
        }

        private Dictionary<string, bag> getBags(string doc) {
            var results = new Dictionary<string, bag>();
            
            var bagMatches = Regex.Matches(doc, "^(.+) (?:bags) contain (.+)$", RegexOptions.Multiline);
            foreach (Match match in bagMatches) {
                bag bp = null;
                if (!results.TryGetValue(match.Groups[1].Value, out bp)) {
                    var name = match.Groups[1].Value;
                    bp = new bag(name);
                    results.Add(name, bp);
                }

                var childMatches = Regex.Matches(match.Groups[2].Value, "([0-9]{1}) (.+?) (?:bag|bags)(?:,|\\.)");
                foreach (Match cm in childMatches) {
                    bag bc = null;
                    if (!results.TryGetValue(cm.Groups[2].Value, out bc)) {
                        var name = cm.Groups[2].Value;
                        bc = new bag(name);
                        results.Add(name, bc);
                    }
                    bp.Children.Add(bc, int.Parse(cm.Groups[1].Value));
                }
            }

            return results;
        }

        public String Answer1() {
            int count = 0;
            foreach (var bag in bags.Values) {
                if (bag.Contains("shiny gold")) {
                    count++;
                }
            }

            return $"{count}";
        }

        public String Answer2() {
            var sgb = bags["shiny gold"];
            return $"{sgb.CountInnerBags()}";
        }
    }
}
