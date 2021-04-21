using System;
using System.Collections.Generic;
using Common.Domain;

namespace Server.Repo.Interfaces
{
    public interface ITransactionRepo : IRepository<int, Transaction>
    {
        
    }
}