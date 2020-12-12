using System;

namespace Aoc2020
{
    partial class Day12 : Day
    {
        class Coordinate {
            public int X {get; set;}
            public int Y {get; set;}

            public void Update(int heading, int value) {
                switch(heading) {
                    case 0:
                        this.Y += value;
                        break;
                    case 180:
                        this.Y -= value;
                        break;
                    case 90:
                        this.X += value;
                        break;
                    case 270:
                        this.X -= value;
                        break;
                }
            }

            public void Rotate(int degrees) {
                if (degrees < 0) {
                    degrees += 360;
                }

                while (degrees > 0) {
                    int x = this.X, y = this.Y;
                    this.X = y;
                    this.Y = -x;
                    degrees -= 90;
                }
            }
        }

        public String Answer1() {
            var coord = new Coordinate() {X = 0, Y = 0};
            var heading = 90;

            foreach (var dir in input) {
                var action = dir[0];
                var value = int.Parse(dir.Substring(1));

                switch(action) {
                    case 'N':
                        coord.Update(0, value);
                        break;
                    case 'S':
                        coord.Update(180, value);
                        break;
                    case 'E':
                        coord.Update(90, value);
                        break;
                    case 'W':
                        coord.Update(270, value);
                        break;
                    case 'L':
                        heading -= value;
                        if (heading < 0) {
                            heading += 360;
                        }
                        break;
                    case 'R':
                        heading += value;
                        if (heading >= 360) {
                            heading -= 360;
                        }
                        break;
                    case 'F':
                        coord.Update(heading, value);
                        break;
                }
            }

            var dist = Math.Abs(coord.X) + Math.Abs(coord.Y);
            return $"{dist}";
        }

        public String Answer2() {
            var coord = new Coordinate() {X = 0, Y = 0};
            var waypoint = new Coordinate() {X = 10, Y = 1};

            foreach (var dir in input) {
                var action = dir[0];
                var value = int.Parse(dir.Substring(1));

                switch(action) {
                    case 'N':
                        waypoint.Update(0, value);
                        break;
                    case 'S':
                        waypoint.Update(180, value);
                        break;
                    case 'E':
                        waypoint.Update(90, value);
                        break;
                    case 'W':
                        waypoint.Update(270, value);
                        break;
                    case 'L':
                        waypoint.Rotate(-value);
                        break;
                    case 'R':
                        waypoint.Rotate(value);
                        break;
                    case 'F':
                        coord.X += waypoint.X * value;
                        coord.Y += waypoint.Y * value;
                        break;
                }
            }

            var dist = Math.Abs(coord.X) + Math.Abs(coord.Y);
            return $"{dist}";
        }
    }
}
