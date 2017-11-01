using Nasa.Mission.Mars.DAL;
using Nasa.Mission.Mars.DAL.Robots;
using Nasa.Mission.Mars.Entity;
using Nasa.Mission.Mars.Services.Robots;
using Nasa.Mission.Mars.WebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nasa.Mission.Mars.WebAPI.Controllers
{
    [RoutePrefix("api/robots")]
    public class RobotServoMotorController : ApiController
    {
        private readonly IRobotRepository _repository;
        private readonly IRobotServoMotorService _servoMotorService;

        public RobotServoMotorController(IRobotRepository repository, IRobotServoMotorService servoMotorService)
        {
            _repository = repository;
            _servoMotorService = servoMotorService;
        }

        [HttpPost, Route("{robotId:int}/turn-right")]
        public HttpResponseMessage TurnRight(int robotId)
        {
            var robot = _repository.Get(robotId);
            var job = _servoMotorService.TurnRight(robot);

            return AcceptedJob(robot, job);
        }
        
        [HttpPost, Route("{robotId:int}/turn-left")]
        public HttpResponseMessage TurnLeft(int robotId)
        {
            var robot = _repository.Get(robotId);
            var job = _servoMotorService.TurnLeft(robot);
            
            return AcceptedJob(robot, job);
        }

        [HttpPost, Route("{robotId:int}/move-forward")]
        public HttpResponseMessage MoveForward(int robotId)
        {
            var robot = _repository.Get(robotId);
            var job = _servoMotorService.MoveForward(robot);

            return AcceptedJob(robot, job);
        }

        [HttpPost, Route("{robotId:int}/put-on-land")]
        public HttpResponseMessage PutOnLand(int robotId, [FromBody]LandingModel landingModel)
        {
            var robot = _repository.Get(robotId);
            var job = _servoMotorService.PutOnLand(robot, landingModel.Position, landingModel.Direction);

            return AcceptedJob(robot, job);
        }

        private HttpResponseMessage AcceptedJob(Robot robot, RobotJob job)
        {
            var result = new HttpResponseMessage(HttpStatusCode.Accepted);
            var location = Url.Link("GetRobotJobById", new { robotId = robot.Id, jobId = job.Id });
            result.Headers.Location = new Uri(location);
            return result;
        }
    }
}