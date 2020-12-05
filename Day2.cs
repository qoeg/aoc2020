using System;
using System.Collections.Generic;

namespace Aoc2020
{
    partial class Day2 : Day
    {
        class policy {
            public int Num1 {get; set;}
            public int Num2 {get; set;}
            public char Letter {get; set;}
        }

        Dictionary<policy, string> entries = null;

        private Dictionary<policy, string> parse(string[] values) {
            var result = new Dictionary<policy, string>();
            foreach (string val in values) {
                var p = new policy();
                var dashPos = val.IndexOf('-');
                var spacePos = val.IndexOf(' ');
                var colonPos = val.IndexOf(':');
                p.Num1 = int.Parse(val.Substring(0, dashPos));
                p.Num2 = int.Parse(val.Substring(dashPos+1, spacePos-(dashPos+1)));
                p.Letter = val.Substring(colonPos-1, 1)[0];
                result.Add(p, val.Substring(colonPos+2));
            }

            return result;
        }

        private bool valid1(string pwd, policy p) {
            int letterCount = 0;
            foreach (char c in pwd.ToCharArray()) {
                if (c == p.Letter)
                    letterCount++;
            }
            return letterCount >= p.Num1 && letterCount <= p.Num2;
        }

        private bool valid2(string pwd, policy p) {
            var char1 = pwd[p.Num1-1];
            var char2 = pwd[p.Num2-1];

            return char1 == p.Letter ^ char2 == p.Letter;
        }

        public Day2() {
            entries = parse(input);
        }

        public String Answer1() {
            int count = 0;
            foreach (var entry in entries) {
                if (valid1(entry.Value, entry.Key)) {
                    count++;
                }
            }

            return count.ToString();
        }

        public String Answer2() {
            int count = 0;
            foreach (var entry in entries) {
                if (valid2(entry.Value, entry.Key)) {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
