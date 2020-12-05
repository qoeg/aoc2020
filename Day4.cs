using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    partial class Day4 : Day
    {
        class passport {
            public string BirthYear {get; set;}
            public string IssueYear {get; set;}
            public string ExpirationYear {get; set;}
            public string Height {get; set;}
            public string HairColor {get; set;}
            public string EyeColor {get; set;}
            public string PassportId {get; set;}
            public string CountryId {get; set;}

            public override string ToString() {
                var sw = new StringWriter();
                sw.WriteLine($"----- Passport {this.PassportId} -----");
                sw.WriteLine($"Birth Year: {this.BirthYear}");
                sw.WriteLine($"Issue Year: {this.IssueYear}");
                sw.WriteLine($"Expiration Year: {this.ExpirationYear}");
                sw.WriteLine($"Height: {this.Height}");
                sw.WriteLine($"Hair Color: {this.HairColor}");
                sw.WriteLine($"Eye Color: {this.EyeColor}");

                return sw.ToString();
            }
        }

        private List<passport> passports = null;

        public Day4() {
            passports = getPassports(input);
        }

        private void writeField(passport pt, string field, string value) {
            switch (field) {
                case "byr":
                    pt.BirthYear = value;
                    break;
                case "iyr":
                    pt.IssueYear = value;
                    break;
                case "eyr":
                    pt.ExpirationYear = value;
                    break;
                case "hgt":
                    pt.Height = value;
                    break;
                case "hcl":
                    pt.HairColor = value;
                    break;
                case "ecl":
                    pt.EyeColor = value;
                    break;
                case "pid":
                    pt.PassportId = value;
                    break;
                case "cid":
                    pt.CountryId = value;
                    break;
            }
        }

        private string getValue(string file, int pos) {
            var result = new List<char> {};
            while (pos < file.Length) {
                var cha = file[pos];
                if (cha == ' ' || cha == '\r')
                    break;

                result.Add(cha);
                pos++;
            }
            return new string(result.ToArray());
        }

        private List<passport> getPassports(string file) {
            var result = new List<passport>();
            var current = new passport();
            
            int pos = -1;
            bool newLine = true;
            while (pos < file.Length-1) {
                pos++;
                var cha = file[pos];

                if (cha == '\r')
                    continue;

                if (cha == '\n' && !newLine) {
                    newLine = true;
                    continue;
                }

                if (cha == '\n' && newLine) {
                    result.Add(current);
                    current = new passport();
                    continue;
                }

                if (cha == ':') {
                    var field = file.Substring(pos-3, 3);
                    var value = getValue(file, pos+1);
                    writeField(current, field, value);
                    pos += value.Length;
                }

                newLine = false;
            }

            result.Add(current);
            return result;
        }

        private bool complete(passport pt) {
            bool result = !string.IsNullOrEmpty(pt.BirthYear);
            result &= !string.IsNullOrEmpty(pt.IssueYear);
            result &= !string.IsNullOrEmpty(pt.ExpirationYear);
            result &= !string.IsNullOrEmpty(pt.Height);
            result &= !string.IsNullOrEmpty(pt.HairColor);
            result &= !string.IsNullOrEmpty(pt.EyeColor);
            result &= !string.IsNullOrEmpty(pt.PassportId);
            //result &= !string.IsNullOrEmpty(pt.CountryId);

            return result;
        }

        private bool valid(passport pt) {
            int byr = int.Parse(pt.BirthYear);
            if (byr < 1920 || byr > 2002)
                return false;

            int iyr = int.Parse(pt.IssueYear);
            if (iyr < 2010 || iyr > 2020)
                return false;

            int eyr = int.Parse(pt.ExpirationYear);
            if (eyr < 2020 || eyr > 2030)
                return false;

            if (pt.Height.Contains("cm")) {
                var num = int.Parse(pt.Height.Substring(0, pt.Height.IndexOf("cm")));
                if (num < 150 || num > 193)
                    return false;
            } else if (pt.Height.Contains("in")) {
                var num = int.Parse(pt.Height.Substring(0, pt.Height.IndexOf("in")));
                if (num < 59 || num > 76)
                    return false;
            } else {
                return false;
            }

            if (!Regex.IsMatch(pt.HairColor, "^#[a-fA-F0-9]{6}$"))
                return false;

            if (pt.EyeColor != "amb"
                && pt.EyeColor != "blu"
                && pt.EyeColor != "brn"
                && pt.EyeColor != "gry"
                && pt.EyeColor != "grn"
                && pt.EyeColor != "hzl"
                && pt.EyeColor != "oth")
                return false;

            if (!Regex.IsMatch(pt.PassportId, "^[0-9]{9}$"))
                return false;

            return true;
        }

        public String Answer1() {
            int count = 0;
            foreach (var p in passports) {
                if (complete(p))
                    count++;
            }

            return count.ToString();
        }

        public String Answer2() {
            int count = 0;
            foreach (var p in passports) {
                if (complete(p) && valid(p)) {
                    count++;
                }
            }

            return $"{count}";
        }
    }
}
