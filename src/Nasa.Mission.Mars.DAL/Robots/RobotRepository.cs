using Nasa.Mission.Mars.Entity;
using Nasa.Mission.Mars.Entity.ModelConstraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.DAL.Robots
{
    public interface IRobotRepository : IRepository<Robot, int>
    {
    }
}
