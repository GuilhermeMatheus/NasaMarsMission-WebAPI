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
using Nasa.Mission.Mars.Entity.ModelConstraints;

namespace Nasa.Mission.Mars.WebAPI.Tests
{
    [TestClass]
    public class RobotExplorationAreaControllerTest
    {
        [TestMethod]
        public void WhenRobotHaveBoundaryConstraintBeforeLanding_LandingOutOfRange_MustThrow()
        {
            CreateControllerAndRobot(out RobotExplorationAreaController controller, out Robot robot);

            controller.Post(robot.Id, new PositionModel { X = 2, Y = 2 });

            robot.PutOnOrbit();
            
            Assert.ThrowsException<ConstraintException>
                (() => robot.PutOnLand(new PositionModel { X = 2, Y = 3 }, Direction.North));
        }

        [TestMethod]
        public void WhenRobotHaveBoundaryConstraint_SetRobotExplorationBoundary_MustThrow()
        {
            CreateControllerAndRobot(out RobotExplorationAreaController controller, out Robot robot);

            controller.Post(robot.Id, new PositionModel { X = 2, Y = 2 });
            Assert.ThrowsException<InvalidOperationException>
                (() => controller.Post(robot.Id, new PositionModel { X = 2, Y = 2 }));
        }

        [TestMethod]
        public void WhenRobotHaveExplorationBoundary_MoveForward_MustThrowWhenBoundaryExceed()
        {
            CreateControllerAndRobot(out RobotExplorationAreaController controller, out Robot robot);

            robot.PutOnOrbit();
            robot.PutOnLand(new PositionModel(), Direction.North);

            controller.Post(robot.Id, new PositionModel { X = 2, Y = 2 });

            robot.MoveForward();
            robot.MoveForward();

            Assert.ThrowsException<ConstraintException>
                (() => robot.MoveForward());
        }

        private void CreateControllerAndRobot(out RobotExplorationAreaController controller, out Robot robot)
        {
            var repo = new DumbRobotRepository();
            var service = new ExplorationAreaService(repo);
            
            controller = new RobotExplorationAreaController(repo, service);
            robot = repo.Get().First();

            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            controller.Configuration = new HttpConfiguration();
            WebApiConfig.Register(controller.Configuration);
        }
    }
}
