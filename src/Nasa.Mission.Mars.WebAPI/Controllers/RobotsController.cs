using Nasa.Mission.Mars.DAL.Robots;
using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nasa.Mission.Mars.WebAPI.Controllers
{
    public class RobotsController : ApiController
    {
        private readonly IRobotRepository _repository;

        public RobotsController(IRobotRepository repository) =>
            _repository = repository;

        public IEnumerable<Robot> Get() =>
            _repository.Get();

        public Robot Get(int id) =>
            _repository.Get(id);
    }
}