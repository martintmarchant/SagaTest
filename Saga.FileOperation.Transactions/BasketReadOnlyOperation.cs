using Saga.Common.Model.Transaction;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Saga.Transaction
{
    public class BasketReadOnlyOperation : IBasketReadOnlyOperation
    {
        private IEnumerable<TransactionDetail> TransactionDetails;
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BasketReadOnlyOperation(Csv.IFileConverter fileConverter,
                                       Model.FileSource csvFileSettings)
        {
            string fileName = Path.Combine(csvFileSettings.FilePath, csvFileSettings.FileName);
            TransactionDetails = fileConverter.FromFile(fileName);
        }

        /// <summary>
        /// Get Transaction Detail by TransactionId
        /// </summary>
        /// <param name="transactionNumber"></param>
        /// <returns></returns>
        public TransactionDetail GetByTransactionId(Guid transactionNumber)
        {
            return TransactionDetails
                        .First(x=> x.TransactionNumber.Equals(transactionNumber));
        }

        /// <summary>
        /// Get all transaction details from Csv file
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public IEnumerable<TransactionDetail> GetAllTransactions(string domain = null)
        {
            var result = TransactionDetails.OrderByDescending(f => f.CreatedDateTime);
            if (!string.IsNullOrEmpty(domain))
            {
                return result.Where(f => f.Domain == domain);
            }
            else
            {
                return result;
            }
        }
    }
}
