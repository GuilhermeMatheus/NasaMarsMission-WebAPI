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
    public class RobotExplorationAreaController : ApiController
    {
        private readonly IExplorationAreaService _areaService;
        private readonly IRobotRepository _repository;

        public RobotExplorationAreaController(IRobotRepository repository, IExplorationAreaService areaService)
        {
            _areaService = areaService;
            _repository = repository;
        }
        
        [Route("{robotId:int}/boundary-area")]
        public HttpResponseMessage Post(int robotId, [FromBody]PositionModel boundary)
        {
            var robot = _repository.Get(robotId);
            var job = _areaService.SetRobotExplorationBoundary(robot, boundary);

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