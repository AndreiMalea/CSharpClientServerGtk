using System;
using Common.Domain;

namespace Common.Networking
{
    [Serializable]
    public class BuyTicketResponse:Response
    {
        private Transaction _transaction;

        public Transaction Transaction
        {
            get => _transaction;
            set => _transaction = value;
        }

        public BuyTicketResponse(Transaction transaction)
        {
            _transaction = transaction;
        }
    }
}