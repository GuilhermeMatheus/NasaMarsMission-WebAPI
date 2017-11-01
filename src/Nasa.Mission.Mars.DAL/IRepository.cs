using System;
using System.Collections.Generic;

namespace Nasa.Mission.Mars.DAL
{
    public interface IRepository<TModel, TModelKey> : IUnitOfWork
    {
        IEnumerable<TModel> Get();
        TModel Get(TModelKey id);

        void Update(TModel item);
        void Create(TModel item);
        void Delete(TModel item);
    }
}
