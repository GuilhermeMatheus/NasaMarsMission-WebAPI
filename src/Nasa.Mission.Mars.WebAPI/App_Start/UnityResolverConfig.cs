using Nasa.Mission.Mars.DAL;
using Nasa.Mission.Mars.DAL.Dumb.Robots;
using Nasa.Mission.Mars.Services.Robots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Nasa.Mission.Mars.WebAPI.App_Start
{
    public class UnityResolverConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            var maps = MapRepositories().Union(MapServices()).ToList();

            foreach (var item in maps)
                container.RegisterType(item.tTo, item.tFrom);
            
            config.DependencyResolver = new UnityResolver(container);
        }

        public static IEnumerable<(Type tFrom, Type tTo)> MapRepositories() =>
            MapAssemblies(
                typeof(IRepository<,>).Assembly,
                typeof(RobotRepository).Assembly);

        public static IEnumerable<(Type tFrom, Type tTo)> MapServices() =>
            MapAssemblies(
                typeof(IRobotServoMotorService).Assembly,
                typeof(RobotServoMotorService).Assembly);

        public static IEnumerable<(Type tFrom, Type tTo)> MapAssemblies(Assembly aFrom, Assembly aTo)
        {
            var _from = aFrom.GetExportedTypes();
            var to = aTo.GetExportedTypes();

            return
                from item in
                    from tTo in to
                    let interfaces = tTo.GetInterfaces()
                    let services = _from.Where(_ => interfaces.Contains(_))
                    select new { tTo, services }
                from tFrom in item.services
                select (item.tTo, tFrom);
        }
    }
}