using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saga.Common.Model.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Saga.Api.Transactions.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        #region local variables
        private ILogger<BasketController> _log;
        private Transaction.Model.FileSource _csvFileSettings;
        private Transaction.Csv.IFileConverter _csvFileConverter;
        private Transaction.IBasketReadOnlyOperation _basketReadOnlyOperation;
        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="csvFileConverter"></param>
        /// <param name="basketReadOnlyOperation"></param>
        /// <param name="csvFileSettings"></param>
        public BasketController(ILogger<BasketController> log,
                                Transaction.Csv.IFileConverter csvFileConverter, 
                                Transaction.IBasketReadOnlyOperation basketReadOnlyOperation,
                                Transaction.Model.FileSource csvFileSettings)
        {
            _log = log;
            _csvFileSettings = csvFileSettings;
            _csvFileConverter = csvFileConverter;
            _basketReadOnlyOperation = basketReadOnlyOperation;
        }


        /// <summary>
        /// Gets all basket transactions from Basket
        /// </summary>
        /// <param name="domain">Optional: applies filter on domain field</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(string domain="")
        {
            try
            {
                _log.LogInformation($"Request Received: Transactions.GetAll {domain}");
                return Ok(_basketReadOnlyOperation.GetAllTransactions(domain).ToList());
            }
            catch (Exception ex)
            {
                _log.LogCritical($"Request FAILED: {ex.Message.ToString()}");
                return BadRequest(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Gets TransactionDetail matching a specified TransactionNumber
        /// </summary>
        /// <param name="TransactionNumber">Guid to locate</param>
        [HttpGet("{transactionNumber}")]
        public IActionResult GetTransactionNumber(string transactionNumber)
        {
            _log.LogInformation($"request received");

            try
            {
                _log.LogInformation($"Request Received: GetTransactionNumber {transactionNumber}");
                return Ok(_basketReadOnlyOperation.GetByTransactionId(Guid.Parse(transactionNumber)));
            }
            catch (Exception ex)
            {
                _log.LogCritical($"Request FAILED: {ex.Message.ToString()}");
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
