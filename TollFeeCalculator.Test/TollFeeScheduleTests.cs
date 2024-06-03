using Xunit;
using System;

namespace TollFeeCalculator.Tests
{
    public class TollFeeScheduleTests
    {
        private TollFeeSchedule _tollFeeSchedule;

        public TollFeeScheduleTests()
        {
            _tollFeeSchedule = new TollFeeSchedule();
        }

        [Fact]
        public void CalculateTollFee_WhenCalledWithDateTime_ReturnsExpectedFee()
        {
            DateTime date = new DateTime(2022, 1, 1, 8, 0, 0); 

            int fee = _tollFeeSchedule.CalculateTollFee(date);

            Assert.Equal(13, fee);
        }
    }
}