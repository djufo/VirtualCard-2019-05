using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualCard.Lib.Services;

namespace VirtualCard.Lib.Tests
{
    [TestClass]
    public class PinServiceTests
    {
        PinService _pinService;

        [TestInitialize]
        public void SetUp()
        {
            _pinService = new PinService();
        }

        [DataTestMethod]
        [DataRow(1234, "mQrGR829Q89HXSRgef/2JA==")]
        [DataRow(3456, "fkdSbbB6NOW/W/gyH/BnkA==")]
        [DataRow(1111, "NDZdTT8IBtJT9lKfxV5ghg==")]
        public void PinToHashTest(int pin, string pinHash)
        {
            string result = _pinService.GetHash(pin);

            Assert.AreEqual(pinHash, result);
        }
    }
}
