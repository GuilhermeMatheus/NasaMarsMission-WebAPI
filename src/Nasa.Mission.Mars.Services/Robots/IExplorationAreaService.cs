using Nasa.Mission.Mars.Entity;

namespace Nasa.Mission.Mars.Services.Robots
{
    public interface IExplorationAreaService
    {
        RobotJob SetRobotExplorationBoundary(Robot robot, Position boundaryPosition);
    }
}