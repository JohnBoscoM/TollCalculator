using Xunit;
using System;

namespace TollFeeCalculator.Tests
{
    public class TollFreeDateTests
    {
        private TollFreeDate _tollFreeDate;

        public TollFreeDateTests()
        {
            _tollFreeDate = new TollFreeDate();
        }

        [Fact]
        public void IsTollFreeDate_WhenCalledWithTollFreeDate_ReturnsTrue()
        {
            DateTime date = new DateTime(2024, 1, 1);

            bool isTollFree = _tollFreeDate.IsTollFreeDate(date);

            Assert.True(isTollFree);
        }

        [Fact]
        public void IsTollFreeDate_WhenCalledWithNonTollFreeDate_ReturnsFalse()
        {
            DateTime date = new DateTime(2024, 1, 2);

            bool isTollFree = _tollFreeDate.IsTollFreeDate(date);

            Assert.False(isTollFree);
        }
    }
}