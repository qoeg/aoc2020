using System;
using System.Collections.Generic;

namespace Aoc2020
{
    partial class Day8 : Day
    {
        class Instruction {
            public string Operation {get; set;}
            public int Argument {get; set;}
            public Instruction(string op, int arg) {
                Operation = op;
                Argument = arg;
            }
        }

        class Result {
            public int Accumulator {get; set;}
            public int ChangedLine {get; set;}
            public int ExitCode {get; set;}
        }

        private Dictionary<int, bool> failed = new Dictionary<int, bool>();

        private List<Instruction> instructions = null;

        public Day8() {
            instructions = getInstructions(input);
        }

        private List<Instruction> getInstructions(string[] lines) {
            var res = new List<Instruction>();
            foreach (var line in lines) {
                var op = line.Substring(0, 3);
                var arg = int.Parse(line.Substring(4));
                res.Add(new Instruction(op, arg));
            }
            return res;
        }

        private Result execute(bool attemptFix = false) {
            var res = new Result();
            var executed = new Dictionary<int, bool>();

            int pos = 0;
            while (pos < instructions.Count) {
                var instr = instructions[pos];

                if (executed.GetValueOrDefault(pos)) {
                    res.ExitCode = 1;
                    break;
                } else {
                    executed[pos] = true;
                }

                switch (instr.Operation) {
                    case "acc":
                        res.Accumulator += instr.Argument;
                        pos++;
                        break;
                    case "jmp":
                        if (attemptFix && res.ChangedLine == 0 && !failed.GetValueOrDefault(pos)) {
                            res.ChangedLine = pos;
                            pos++;
                        } else {
                            pos += instr.Argument;
                        }
                        break;
                    case "nop":
                        if (attemptFix && res.ChangedLine == 0 && !failed.GetValueOrDefault(pos)) {
                            res.ChangedLine = pos;
                            pos += instr.Argument;
                        } else {
                            pos++;
                        }
                        break;
                }
            }
            return res;
        }

        public String Answer1() {
            var res = execute();

            return $"{res.Accumulator}";
        }

        public String Answer2() {
            Result res;
            do {
                res = execute(true);
                if (res.ExitCode > 0) {
                    failed[res.ChangedLine] = true;
                }
            } while (res.ExitCode > 0);

            return $"{res.Accumulator}";
        }
    }
}
