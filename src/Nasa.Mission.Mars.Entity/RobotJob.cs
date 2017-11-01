using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Entity
{
    public class RobotJob
    {
        public int Id { get; private set; }
        public string Status => "done";

        public RobotJob(int id) =>
            Id = id;

        //TODO: remove this
        private static Random _rdn = new Random();
        public static RobotJob Random() =>
            new RobotJob(_rdn.Next());
    }
}
