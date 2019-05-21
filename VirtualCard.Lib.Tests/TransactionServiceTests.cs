using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualCard.Lib.Exceptions;
using VirtualCard.Lib.Models;
using VirtualCard.Lib.Services;

namespace VirtualCard.Lib.Tests
{
    [TestClass]
    public class TransactionServiceTests
    {
        private Mock<IPinService> _pinServiceMock;
        private Mock<IAccountService> _accountServiceMock;
        private TransactionService _transactionService;

        private Account _acc1;

        [TestInitialize]
        public void SetUp()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _acc1 = new Account(new List<Card> { new Card("1111111111111111", "1111"), new Card("2222222222222222", "2222") }, 100);
            _transactionService = new TransactionService(_accountServiceMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(CardException))]
        public void Transaction_Fail_GetAccountFail()
        {
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<int>(), It.IsAny<string>())).Throws<CardException>();

            Request request = new Request();
            Response response = _transactionService.Submit(request);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientBalance))]
        public void Transaction_Fail_GetAccountSuccess_CreditToLarge()
        {
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<int>(), It.IsAny<string>())).Returns(_acc1);

            Request request = new Request
            {
                Amount = -101,
                CardNumber = "xxxxxxxxxxx",
                Pin = 9999
            };
            Response response = _transactionService.Submit(request);
        }

        [TestMethod]
        public void Transaction_Success_GetAccountSuccess_CreditCorrect()
        {
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<int>(), It.IsAny<string>())).Returns(_acc1);

            Request request = new Request
            {
                Amount = -10,
                CardNumber = "xxxxxxxxxxx",
                Pin = 9999
            };
            Response response = _transactionService.Submit(request);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(90, response.Balance);

            request.Amount = -90;
            response = _transactionService.Submit(request);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(0, response.Balance);
        }

        [TestMethod]
        public void Transaction_Success_GetAccountSuccess_Debit()
        {
            _accountServiceMock.Setup(x => x.GetAccount(It.IsAny<int>(), It.IsAny<string>())).Returns(_acc1);

            Request request = new Request
            {
                Amount = 1000,
                CardNumber = "xxxxxxxxxxx",
                Pin = 9999
            };
            Response response = _transactionService.Submit(request);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1100, response.Balance);
        }
    }
}
