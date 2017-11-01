using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Entity
{
    public struct Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position ShiftX(int shift) => new Position(X + shift, Y);
        public Position ShiftY(int shift) => new Position(X, Y + shift);

        public static Position operator +(Position l, Position r) =>
            new Position(r.X + l.X, r.Y + l.Y);
    }
}
