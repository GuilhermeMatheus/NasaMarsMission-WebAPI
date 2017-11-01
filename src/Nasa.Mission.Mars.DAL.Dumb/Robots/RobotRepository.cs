using Nasa.Mission.Mars.DAL.Robots;
using Nasa.Mission.Mars.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nasa.Mission.Mars.DAL.Dumb.Robots
{
    public class RobotRepository : IRobotRepository
    {
        private static readonly Robot[] _data = {
            new Robot(1, "Heisenberg", "Exploration Rover")
        };

        static RobotRepository()
        {
            foreach (var item in _data)
                item.PutOnOrbit();
        }

        public IEnumerable<Robot> Get() => _data;

        public Robot Get(int id) =>
            Get().FirstOrDefault(_ => _.Id == id);

        public void Commit() { }
        public void Create(Robot item) { }
        public void Delete(Robot item) { }
        public IUnitOfWork GetUnitOfWork() => null;
        public void SetUnitOfWork(IUnitOfWork uow) { }
        public void Update(Robot item) { }
    }
}
