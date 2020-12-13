using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc2020.Utils
{
    struct Coordinate {
        public int X {get; set;}
        public int Y {get; set;}
    }

    interface ICell {
        Coordinate Position {get; set;}
        string Print();
    }

    interface ICell<T> : ICell {
        T Value {get; set;}
    }

    class CharCell : ICell<char> {
        public Coordinate Position {get; set;}
        public char Value {get; set;}
        public string Print() {
            return Value.ToString();
        }
    }
    
    class Grid<T> {
        private List<List<ICell<T>>> grid;
        private Dictionary<Coordinate, List<ICell<T>>> adjacentCache;
        public Dictionary<Coordinate, ICell<T>> Cells {get; set;}

        public Grid() {
            grid = new List<List<ICell<T>>>();
            adjacentCache = new Dictionary<Coordinate, List<ICell<T>>>();
            Cells = new Dictionary<Coordinate, ICell<T>>();
        }

        public void Add(ICell<T> cell) {
            if (grid.Count <= cell.Position.X) {
                int diff = cell.Position.X - grid.Count + 1;
                while (diff > 0) {
                    grid.Add(new List<ICell<T>>());
                    diff--;
                }
            }

            if (grid[cell.Position.X].Count <= cell.Position.Y) {
                int diff = cell.Position.Y - grid[cell.Position.X].Count + 1;
                while (diff > 1) {
                    grid[cell.Position.X].Add(null);
                    diff--;
                }
                grid[cell.Position.X].Add(cell);
            } else {
                grid[cell.Position.X][cell.Position.Y] = cell;
            }
            
            Cells[cell.Position] = cell;
        }

        public List<ICell<T>> GetAdjacent(Coordinate pos) {
            var hit = adjacentCache.GetValueOrDefault(pos);
            if (hit != null) {
                return hit;
            }

            var cells = new List<ICell<T>>();
            for (int dx = -1; dx <= 1; dx++) {
                for (int dy = -1; dy <= 1; dy++) {
                    if (dx == 0 && dy == 0) {
                        continue;
                    }

                    var coord = new Coordinate{X = pos.X + dx, Y = pos.Y + dy};
                    var cell = Cells.GetValueOrDefault(coord);
                    if (cell != null) {
                        cells.Add(cell);
                    }
                }
            }
            
            adjacentCache.Add(pos, cells);
            return cells;
        }

        public List<ICell<T>> GetAll(T value) {
            var res = new List<ICell<T>>();
            foreach (var cell in Cells.Values) {
                if (cell.Value.Equals(value)) {
                    res.Add(cell);
                }
            }
            return res;
        }

        public string Print() {
            if (grid.Count == 0) {
                return string.Empty;
            }

            var sb = new StringBuilder();
            for (int y = 0; y < grid[0].Count; y++) {
                for (int x = 0; x < grid.Count; x++) {
                    sb.Append(grid[x][y].Print());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}