using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.DAL
{
    public interface IUnitOfWork
    {
        void SetUnitOfWork(IUnitOfWork uow);
        IUnitOfWork GetUnitOfWork();
        void Commit();
    }
}
