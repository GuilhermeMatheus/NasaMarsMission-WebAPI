using Nasa.Mission.Mars.DAL;
using Nasa.Mission.Mars.DAL.Robots;
using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.Services.Robots
{
    public class ExplorationAreaService : IExplorationAreaService
    {
        private readonly IRobotRepository _repository;
        public ExplorationAreaService(IRobotRepository repository)
        {
            _repository = repository;
        }

        public RobotJob SetRobotExplorationBoundary(Robot robot, Position boundaryPosition)
        {
            return DoInUnitOfWork(() =>
            {
                var constraint = new BoundaryPositionConstraint(boundaryPosition);
                robot.ConstraintValidator.AddUniqueConstraint(_ => _.Position, constraint);
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

