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
    public class RobotsControllerTest
    {
        //GetById
        //Get

        private void CreateControllerAndRobot(out RobotsController controller, out Robot robot)
        {
            var repo = new DumbRobotRepository();

            controller = new RobotsController(repo);
            robot = repo.Get().First();

            controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");
            controller.Configuration = new HttpConfiguration();
            WebApiConfig.Register(controller.Configuration);
        }
    }
}
