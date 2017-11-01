using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Entity.Utils
{
    public static class DirectionUtils
    {
        public static Direction Right(this Direction direction) =>
            (Direction)Shift(direction, 1);

        public static Direction Left(this Direction direction) =>
            (Direction)Shift(direction, 3);

        private static int Shift(Direction direction, int delta) =>
            ((int)direction + delta) % 4;
    }
}
