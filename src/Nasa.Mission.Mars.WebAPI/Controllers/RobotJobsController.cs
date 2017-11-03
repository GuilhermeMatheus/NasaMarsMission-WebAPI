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
        //Uma vez que esse conceito foge do escopo
        //do projeto, vamos sempre retornar um JOB
        //completado.
        [Route("{robotId:int}/jobs/{jobId:int}")]
        public RobotJob Get(int robotId, int jobId) =>
            new RobotJob(jobId);
    }
}