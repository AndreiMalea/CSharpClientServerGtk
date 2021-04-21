using System.Collections.Generic;

namespace Server.Repo.Interfaces
{
    public interface IRepository<TId, TE>
    {
        TE FindOne(TId id);
        IDictionary<TId, TE> FindAll();
        IList<TE> GetAllList();
        TE Save(TE entity);
        TE Delete(TId id);
        TE Update(TE entity);
    }
}