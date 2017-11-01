using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nasa.Mission.Mars.WebAPI.Controllers
{
    [RoutePrefix("api/robots")]
    public class RobotJobsController : ApiController
    {
        [Route("{robotId:int}/jobs/{jobId:int}")]
        public RobotJob Get(int robotId, int jobId) =>
            new RobotJob(jobId);
    }
}