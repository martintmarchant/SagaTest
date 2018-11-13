using Microsoft.Extensions.Logging;
using Saga.Common.File;
using Saga.Common.Model.Transaction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Saga.Transaction.Csv
{
    public class FileConverter : IFileConverter
    {
        private ILogger<FileConverter> _log;

        public FileConverter(ILogger<FileConverter> log)
        {
            _log = log;
        }

        /// <summary>
        /// Loads the content of a CSV file containing TransactionDetail records into a TransactionDetail enumerable.
        /// </summary>
        /// <param name="fullFileName"></param>
        /// <returns></returns>
        public IEnumerable<TransactionDetail> FromFile(string fullFileName)
        {
            if (File.Exists(fullFileName))
            {
                _log.LogInformation($"Reading {fullFileName}.");
                string[] contentLinesStringArray = Reader.ContentToStringArray(fullFileName);

                return FromStringArray(contentLinesStringArray);
            }
            else
            {
                _log.LogCritical($"File Not found: {fullFileName} - When running Saga.Transaction.Csv.FileConverter.FromFile");
                throw new Exception($"Unable to find file {fullFileName}.  Please verify the path/access permissions.");
            }
        }


        /// <summary>
        /// Converts a list of CSV seperated values into a Enumerable of TransactionDetail
        /// </summary>
        /// <param name="contentLinesStringArray"></param>
        /// <returns></returns>
        public IEnumerable<TransactionDetail> FromStringArray(string[] contentLinesStringArray)
        {
            try
            {
                IEnumerable<TransactionDetail> transactions = 
                     (contentLinesStringArray
                        .Skip(1)
                        .Where(x=> !string.IsNullOrEmpty(x))
                        .Select(x => x.Split(','))
                        .Select(x => new TransactionDetail()
                        {
                            TransactionNumber = Guid.Parse(x[0]),
                            NumberOfPassengers = (x[1] == "" ? (int?)null : int.Parse(x[1])),
                            Domain = x[2],
                            AgentId = x[3],
                            ReferrerUrl = x[4],
                            CreatedDateTime = DateTime.Parse(x[5]),
                            UserId = x[6],
                            SelectedCurrency = x[7],
                            ReservationSystem = x[8]
                        }));

                _log.LogDebug($"{transactions.Count()} Records Processed.");
                return transactions;
            }
            catch (Exception ex)
            {
                _log.LogCritical($"Validation Error found in source - When running Saga.Transaction.Csv.FileConverter.FromStringArray.");
                throw new Exception($"File validation failed.", ex);
            }
        }
    }
}
