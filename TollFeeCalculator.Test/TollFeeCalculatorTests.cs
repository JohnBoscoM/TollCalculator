using System;
using Xunit;
using TollFeeCalculator;
using System.Runtime.ConstrainedExecution;

namespace TollFeeCalculator.Tests
{
    public class TollCalculatorTests
    {
        private readonly ITollFeeSchedule _tollFeeSchedule = new TollFeeSchedule();
        private readonly ITollFreeDate _tollFreeDate = new TollFreeDate();
        private readonly TollCalculator _tollCalculator;

        public TollCalculatorTests()
        {
            _tollCalculator = new TollCalculator(_tollFreeDate, _tollFeeSchedule);
        }

        [Fact]
        public void Vehicle_IsTollFree_ShouldReturnZeroFee()
        {
            Vehicle vehicle = new Motorbike();
            DateTime[] dates = { DateTime.Today.AddHours(7) };

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(0, fee);
        }

        [Fact]
        public void TollFreeDate_ShouldReturnZeroFee()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = { new DateTime(2024, 1, 1, 7, 0, 0) };

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(0, fee);
        }

        [Fact]
        public void SinglePassage_ShouldReturnCorrectFee()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = { DateTime.Today.AddHours(7) }; 

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(18, fee);
        }
    }
}
