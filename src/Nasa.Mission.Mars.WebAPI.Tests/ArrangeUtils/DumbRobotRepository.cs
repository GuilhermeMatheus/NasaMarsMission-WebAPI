using Nasa.Mission.Mars.DAL.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nasa.Mission.Mars.DAL;
using Nasa.Mission.Mars.Entity;

namespace Nasa.Mission.Mars.WebAPI.Tests.ArrangeUtils
{
    internal class DumbRobotRepository : IRobotRepository
    {
        private Robot Robot = new Robot(1, "Vince", "Test robot");
        
        public IEnumerable<Robot> Get()
        {
            yield return Robot;
        }

        public Robot Get(int id) =>
            id == Robot.Id
                ? Robot
                : throw new ArgumentException();

        public IUnitOfWork GetUnitOfWork()
        {
            return null;
        }


        public void Commit() { }
        public void Create(Robot item) { }
        public void Delete(Robot item) { }
        public void SetUnitOfWork(IUnitOfWork uow) { }
        public void Update(Robot item) { }
    }
}
