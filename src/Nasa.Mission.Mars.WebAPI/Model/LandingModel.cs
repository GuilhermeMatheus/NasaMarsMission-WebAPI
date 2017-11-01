using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nasa.Mission.Mars.WebAPI.Model
{
    public class LandingModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public Position Position => new Position(X, Y);
    }
}