using Nasa.Mission.Mars.Entity;

namespace Nasa.Mission.Mars.Services.Robots
{
    public interface IRobotServoMotorService
    {
        RobotJob MoveForward(Robot robot);
        RobotJob PutOnLand(Robot robot, Position destination, Direction direction);
        RobotJob TurnLeft(Robot robot);
        RobotJob TurnRight(Robot robot);
    }
}