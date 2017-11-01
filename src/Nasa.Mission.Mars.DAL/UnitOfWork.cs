using System;
using System.Collections.Generic;
using System.Text;

namespace Nasa.Mission.Mars.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IUnitOfWork[] _uows;
        public UnitOfWork(params IUnitOfWork[] uows)
        {
            _uows = uows;
            foreach (var item in uows)
                item.SetUnitOfWork(this);
        }

        public IUnitOfWork GetUnitOfWork() => this;
        public void SetUnitOfWork(IUnitOfWork uow) =>
            throw new NotImplementedException();

        public void CommitAll()
        {
            // commit code ...
            foreach (var item in _uows)
                item.SetUnitOfWork(null);
        }
        public void Commit() => CommitAll();
        public void Dispose() => Commit();
    }
}
