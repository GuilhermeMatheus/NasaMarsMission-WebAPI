using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nasa.Mission.Mars.WebAPI.Controllers;
using Nasa.Mission.Mars.WebAPI.Tests.ArrangeUtils;
using Nasa.Mission.Mars.DAL.Robots;
using Nasa.Mission.Mars.WebAPI.Model;
using Nasa.Mission.Mars.Entity;
using System.Net.Http;
using System.Web.Http;
using Nasa.Mission.Mars.Services.Robots;

namespace Nasa.Mission.Mars.WebAPI.Tests
{
    [TestClass]
    public class RobotNavigationTest
    {
        /*
         * 5 5
         * 1 2 N
         * LMLMLMLMM
         */
        [TestMethod]
        public void RobotNavigationCase1()
        {
            TestCase(
                // 5 5
                new PositionModel { X = 5, Y = 5 },
                // 1 2 N
                new LandingModel { X = 1, Y = 2, Direction = Direction.North },
                // 1 3 N
                new Position(1, 3),
                Direction.North,
                "LMLMLMLMM");
        }

        /*
         * 5 5
         * 3 3 E
         * MMRMMRMRRM
         */
        [TestMethod]
        public void RobotNavigationCase2()
        {
            TestCase(
                // 5 5
                new PositionModel { X = 5, Y = 5 },
                // 3 3 E
                new LandingModel { X = 3, Y = 3, Direction = Direction.East },
                // 5 1 E
                new Position(5, 1),
                Direction.East,
                "MMRMMRMRRM");
        }

        private void TestCase(
            PositionModel robotBoundary, 
            LandingModel landingModel, 
            Position expectedPosition, 
            Direction expectedDirection,
            string commands)
        {
            CreateControllerAndRobot(
                out RobotServoMotorController servoMotorController,
                out RobotExplorationAreaController explorationAreaController,
                out Robot robot);

            robot.PutOnOrbit();

            explorationAreaController.Post(robot.Id, robotBoundary);
            servoMotorController.PutOnLand(robot.Id, landingModel);
            ExecuteCommands(commands, servoMotorController, robot);

            Assert.AreEqual(expectedPosition, robot.Position);
            Assert.AreEqual(expectedDirection, robot.Direction);
        }

        private void ExecuteCommands(string commands, RobotServoMotorController servoMotorController, Robot robot)
        {
            foreach (var item in commands.ToUpper())
                switch (item)
                {
                    case 'M':
                        servoMotorController.MoveForward(robot.Id);
                        break;
                    case 'L':
                        servoMotorController.TurnLeft(robot.Id);
                        break;
                    case 'R':
                        servoMotorController.TurnRight(robot.Id);
                        break;
                    default:
                        break;
                }
        }

        private void CreateControllerAndRobot(out RobotServoMotorController controller, out RobotExplorationAreaController explorationAreaController, out Robot robot)
        {
            var repo = new DumbRobotRepository();
            var service = new DumbRobotServoMotorService();

            controller = new RobotServoMotorController(repo, service);
            robot = repo.Get().First();

            explorationAreaController = new RobotExplorationAreaController(repo, new ExplorationAreaService(repo));

            explorationAreaController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            explorationAreaController.Configuration = new HttpConfiguration();

            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");

            WebApiConfig.Register(controller.Configuration);
            WebApiConfig.Register(explorationAreaController.Configuration);
        }
    }
}
