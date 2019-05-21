using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VirtualCard.Lib.Exceptions;
using VirtualCard.Lib.Models;
using VirtualCard.Lib.Services;

namespace VirtualCard.Lib.Tests
{
    [TestClass]
    public class AccountServiceTests
    {
        private Mock<IPinService> _pinServiceMock;
        private AccountService _accountService;

        [TestInitialize]
        public void SetUp()
        {
            _pinServiceMock = new Mock<IPinService>();
            _accountService = new AccountService(_pinServiceMock.Object);
        }

        [TestMethod]
        public void AddAccount_AddNew()
        {
            Account acc = new Account(new List<Card> { new Card("1111111111111111", "1111") }, 0);
            _accountService.AddAccount(acc);

            acc = new Account(new List<Card> { new Card("1111111111111112", "1111") }, 0);
            _accountService.AddAccount(acc);

            Assert.AreEqual(2, _accountService.Accounts.Count);
            Assert.AreEqual(acc.Cards[0].Number, _accountService.Accounts[1].Cards[0].Number);
        }

        [TestMethod]
        [ExpectedException(typeof(CardExistsException))]
        public void AddAccount_SameCardNo_Fail()
        {
            Account acc = new Account(new List<Card> { new Card("1111111111111111", "1111") }, 0);
            _accountService.AddAccount(acc);

            acc = new Account(new List<Card> { new Card("1111111111111111", "1111") }, 0);
            _accountService.AddAccount(acc);
        }

        [TestMethod]
        public void GetAccount_FindAccount_CardPinMatches()
        {
            string cardNo = "1111111111111111";
            Account acc1 = new Account(new List<Card> { new Card("1111111111111111", "1111") }, 0);
            _accountService.AddAccount(acc1);

            Account acc2 = new Account(new List<Card> { new Card("1111111111111112", "1111") }, 0);
            _accountService.AddAccount(acc2);

            _pinServiceMock.Setup(x => x.GetHash(It.Is<int>(i => i == 1111))).Returns("1111");

            Account result = _accountService.GetAccount(1111, cardNo);

            Assert.AreEqual(cardNo, result.Cards[0].Number);
            _pinServiceMock.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(CardException))]
        public void GetAccount_FailToFindAccount_CardPinNotMatches()
        {
            string cardNo = "1111111111111111";
            Account acc1 = new Account(new List<Card> { new Card("1111111111111111", "1111") }, 0);
            _accountService.AddAccount(acc1);

            Account acc2 = new Account(new List<Card> { new Card("1111111111111112", "1111") }, 0);
            _accountService.AddAccount(acc2);

            _pinServiceMock.Setup(x => x.GetHash(It.Is<int>(i => i == 1111))).Returns("0000");

            Account result = _accountService.GetAccount(1111, cardNo);
        }
    }
}
