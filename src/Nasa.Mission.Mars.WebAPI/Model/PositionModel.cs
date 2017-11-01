using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nasa.Mission.Mars.WebAPI.Model
{
    public class PositionModel
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static implicit operator Position(PositionModel d) =>
            new Position(d.X, d.Y);
    }
}