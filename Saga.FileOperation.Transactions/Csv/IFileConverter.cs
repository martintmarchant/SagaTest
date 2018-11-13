using Saga.Common.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Transaction.Csv
{
    public interface IFileConverter
    {
        IEnumerable<TransactionDetail> FromFile(string fullFileName);
        IEnumerable<TransactionDetail> FromStringArray(string[] contentLinesStringArray);
    }
}
