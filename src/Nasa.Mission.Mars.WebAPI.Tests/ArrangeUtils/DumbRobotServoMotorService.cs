using Nasa.Mission.Mars.Services.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nasa.Mission.Mars.Entity;

namespace Nasa.Mission.Mars.WebAPI.Tests.ArrangeUtils
{
    public class DumbRobotServoMotorService : IRobotServoMotorService
    {
        public RobotJob MoveForward(Robot robot)
        {
            robot.MoveForward();
            return RobotJob.Random();
        }

        public RobotJob PutOnLand(Robot robot, Position destination, Direction direction)
        {
            robot.PutOnLand(destination, direction);
            return RobotJob.Random();
        }

        public RobotJob TurnLeft(Robot robot)
        {
            robot.TurnLeft();
            return RobotJob.Random();
        }

        public RobotJob TurnRight(Robot robot)
        {
            robot.TurnRight();
            return RobotJob.Random();
        }
    }
}
