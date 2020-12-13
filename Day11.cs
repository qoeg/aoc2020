using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2020.Utils;

namespace Aoc2020
{
    static class GridExtensions
    {
        public static List<ICell<char>> GetVisible(this Grid<char> grid, Coordinate pos) {
            var res = new List<ICell<char>>();
            var vectors = new List<(int X, int Y)> {
                (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1)
            };

            foreach (var vector in vectors) {
                ICell<char> vis;
                Coordinate coord = pos;
                do {
                    coord.X += vector.X;
                    coord.Y += vector.Y;
                    vis = grid.Cells.GetValueOrDefault(coord);
                    if (vis == null) {
                        break;
                    }
                } while(vis.Value == '.');

                if (vis != null) {
                    res.Add(vis);
                }
            }
            return res;
        }
    }

    partial class Day11 : Day
    {
        private Grid<char> Run(Grid<char> grid, Func<Coordinate, List<ICell<char>>> getOtherSeats, int threshold) {
            var seatCache = new Dictionary<Coordinate, List<ICell<char>>>();

            List<(ICell<char> Cell, char Value)> changes;
            do {
                changes = new List<(ICell<char>, char)>();
                foreach (var cell in grid.Cells.Values) {
                    if (cell.Value == '.') {
                        continue;
                    }

                    var seats = seatCache.GetValueOrDefault(cell.Position);
                    if (seats == null) {
                        seats = getOtherSeats(cell.Position);
                        seatCache.Add(cell.Position, seats);
                    }

                    switch (cell.Value) {
                        case 'L':
                            if (seats.All(c => c.Value != '#')) {
                                changes.Add(new (cell, '#'));
                            }
                            break;
                        case '#':
                            if (seats.Count(c => c.Value == '#') >= threshold) {
                                changes.Add(new (cell, 'L'));
                            }
                            break;
                    }
                }

                foreach (var change in changes) {
                    change.Cell.Value = change.Value;
                }
            } while (changes.Count > 0);

            return grid;
        }

        public String Answer1() {
            var grid = Parse.CharGrid(input);
            grid = Run(grid, grid.GetAdjacent, 4);

            var occupied = grid.GetAll('#');
            return $"{occupied.Count}";
        }

        public String Answer2() {
            var grid = Parse.CharGrid(input);
            grid = Run(grid, grid.GetVisible, 5);

            var occupied = grid.GetAll('#');
            return $"{occupied.Count}";
        }
    }
}
