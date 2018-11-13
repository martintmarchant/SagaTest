using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Saga.Api.Transactions.Controllers;

namespace Saga.UnitTest.Api.Transactions
{
    [TestClass]
    public class BasketControllerUnitTest
    {

        [TestMethod]
        public void GetAll_RetrieveAllDataNoFliterTest()
        {
            var basketController = GetBasketControllerForUnitTest();
            IActionResult output = basketController.GetAll();
            Assert.IsInstanceOfType(output, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [TestMethod]
        public void GetAll_DomainFilterTest()
        {
            var basketController = GetBasketControllerForUnitTest();
            IActionResult output = basketController.GetAll("10");
            Assert.IsInstanceOfType(output, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }


        [TestMethod]
        public void GetTransactionNumber_InvalidParameterTest()
        {
            var basketController = GetBasketControllerForUnitTest();
            IActionResult output = basketController.GetTransactionNumber("invalid guid");
            Assert.IsInstanceOfType(output, typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }


        [TestMethod]
        public void GetTransactionNumber_ValidGuidTest()
        {
            var basketController = GetBasketControllerForUnitTest();
            IActionResult output = basketController.GetTransactionNumber(System.Guid.NewGuid().ToString());
            Assert.IsInstanceOfType(output, typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }


        private BasketController GetBasketControllerForUnitTest()
        {
            var logger = Mock.Of<ILogger<BasketController>>();
            var csvFileConverter = Mock.Of<Transaction.Csv.IFileConverter>();
            var basketReadOnlyOperation = Mock.Of<Transaction.IBasketReadOnlyOperation>();
            var csvFileSettings = new Saga.Transaction.Model.FileSource() { };
            var basketController = new BasketController(logger, csvFileConverter,basketReadOnlyOperation,csvFileSettings);
            return basketController;
        }

    }
}
