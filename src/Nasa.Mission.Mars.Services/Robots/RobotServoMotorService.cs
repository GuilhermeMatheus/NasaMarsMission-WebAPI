using Nasa.Mission.Mars.DAL;
using Nasa.Mission.Mars.DAL.Robots;
using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Nasa.Mission.Mars.Entity.ModelConstraints;

namespace Nasa.Mission.Mars.Services.Robots
{
    public class RobotServoMotorService : IRobotServoMotorService
    {
        private readonly IRobotRepository _repository;
        public RobotServoMotorService(IRobotRepository repository)
        {
            _repository = repository;
        }

        public RobotJob TurnRight(Robot robot)
        {
            return DoInUnitOfWork(() =>
                robot.TurnRight()
            );
        }
        public RobotJob TurnLeft(Robot robot)
        {
            return DoInUnitOfWork(() =>
                robot.TurnLeft()
            );
        }
        public RobotJob MoveForward(Robot robot)
        {
            return DoInUnitOfWork(() => {
                robot.MoveForward();
            });
        }
        public RobotJob PutOnLand(Robot robot, Position destination, Direction direction)
        {
            return DoInUnitOfWork(() => {
                robot.PutOnLand(destination, direction);
            });
        }
        
        private RobotJob DoInUnitOfWork(Action action)
        {
            using (new UnitOfWork(_repository))
                action();
            return RobotJob.Random();
        }
    }
}
