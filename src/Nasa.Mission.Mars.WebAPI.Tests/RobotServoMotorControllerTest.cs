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

namespace Nasa.Mission.Mars.WebAPI.Tests
{
    [TestClass]
    public class RobotServoMotorControllerTest
    {
        [TestMethod]
        public void WhenRobotJourneyStatusIsOnTravel_PutOnLand_ShouldThrow()
        {
            CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);

            Assert.AreEqual(JourneyStatus.OnTravel, robot.JourneyStatus);
            Assert.ThrowsException<InvalidOperationException>
                (() => robot.PutOnLand(new Position(), Direction.North));
        }

        [TestMethod]
        public void WhenRobotIsOnLand_PutOnLand_ShouldThrow()
        {
            CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);

            robot.PutOnOrbit();
            robot.PutOnLand(new Position(), Direction.North);

            Assert.ThrowsException<InvalidOperationException>
                (() => robot.PutOnLand(new Position(), Direction.North));
        }

        [TestMethod]
        public void WhenDirectionIsNorth_MoveForward_ShouldOffsetPositive1InYPosition()
        {
            var initialPosition = new Position(0, 0);
            var direction = Direction.North;

            var robot = CreateRobotAndMoveForward(initialPosition, direction);

            Assert.AreEqual(initialPosition.Y+1, robot.Position.Y);
        }

        [TestMethod]
        public void WhenDirectionIsSouth_MoveForward_ShouldOffsetNegative1InYPosition()
        {
            var initialPosition = new Position(0, 0);
            var direction = Direction.South;

            var robot = CreateRobotAndMoveForward(initialPosition, direction);

            Assert.AreEqual(initialPosition.Y - 1, robot.Position.Y);
        }

        [TestMethod]
        public void WhenDirectionIsEast_MoveForward_ShouldOffsetPositive1InYPosition()
        {
            var initialPosition = new Position(0, 0);
            var direction = Direction.East;

            var robot = CreateRobotAndMoveForward(initialPosition, direction);

            Assert.AreEqual(initialPosition.X + 1, robot.Position.X);
        }

        [TestMethod]
        public void WhenDirectionIsWeast_MoveForward_ShouldOffsetNegative1InYPosition()
        {
            var initialPosition = new Position(0, 0);
            var direction = Direction.West;

            var robot = CreateRobotAndMoveForward(initialPosition, direction);

            Assert.AreEqual(initialPosition.X - 1, robot.Position.X);
        }
        
        [TestMethod]
        public void WhenRobotIsOnOrbit_PutOnLand_ShouldUpdateRobotAndRespectArguments()
        {
            CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);
            
            var landingModel = new LandingModel { X = 5, Y = 5, Direction = Direction.West };

            robot.PutOnOrbit();
            controller.PutOnLand(robot.Id, landingModel);
            
            Assert.AreEqual(Direction.West, robot.Direction);
            Assert.AreEqual(5, robot.Position.X);
            Assert.AreEqual(5, robot.Position.Y);
        }

        [TestMethod]
        public void WhenRobotDirectionIsNorth_TurnRight_ShouldChangeDirectionToEast()
        {
            CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);

            robot.PutOnOrbit();
            robot.PutOnLand(new Position(), Direction.North);


            controller.TurnRight(robot.Id);
            
            Assert.AreEqual(Direction.East, robot.Direction);
        }

        [TestMethod]
        public void WhenRobotDirectionIsNorth_TurnLeft_ShouldChangeDirectionToWeast()
        {
            CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);

            robot.PutOnOrbit();
            robot.PutOnLand(new Position(), Direction.North);

            controller.TurnLeft(robot.Id);

            Assert.AreEqual(Direction.West, robot.Direction);
        }
        
        [TestMethod]
        public void InAnyRobotDirectionWhenCalling_TurnLeftOrRight4Times_FinalDirectionMustBeTheSame()
        {
            var directions = new[] {
                Direction.North,
                Direction.East,
                Direction.South,
                Direction.West
            };

            foreach (var direction in directions)
            {
                CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);

                robot.PutOnOrbit();
                robot.PutOnLand(new Position(), direction);

                for (int i = 0; i < 4; i++)
                    controller.TurnLeft(robot.Id);
                Assert.AreEqual(direction, robot.Direction);

                for (int i = 0; i < 4; i++)
                    controller.TurnRight(robot.Id);
                Assert.AreEqual(direction, robot.Direction);
            }
        }
        
        private Robot CreateRobotAndMoveForward(Position initialPosition, Direction direction)
        {
            CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot);

            robot.PutOnOrbit();
            robot.PutOnLand(initialPosition, direction);

            controller.MoveForward(robot.Id);
            return robot;
        }
        private void CreateControllerAndRobot(out RobotServoMotorController controller, out Robot robot)
        {
            var repo = new DumbRobotRepository();
            var service = new DumbRobotServoMotorService();

            controller = new RobotServoMotorController(repo, service);
            robot = repo.Get().First();

            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            controller.Configuration = new HttpConfiguration();
            WebApiConfig.Register(controller.Configuration);
        }
    }
}
