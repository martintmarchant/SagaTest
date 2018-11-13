using Saga.Common.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Transaction
{
    public interface IBasketReadOnlyOperation
    {
        TransactionDetail GetByTransactionId(Guid transactionNumber);
        IEnumerable<TransactionDetail> GetAllTransactions(string domain = null);
    }
}
