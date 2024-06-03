using System;
using Xunit;
using TollFeeCalculator;
using Moq;

namespace TollFeeCalculator.Tests
{
    public class TollCalculatorTests
    {
        private readonly Mock<ITollFeeSchedule> _tollFeeSchedule = new Mock<ITollFeeSchedule>();
        private readonly Mock<ITollFreeDate> _tollFreeDate = new Mock<ITollFreeDate>();
        private readonly TollCalculator _tollCalculator;

        public TollCalculatorTests()
        {
            _tollCalculator = new TollCalculator(_tollFreeDate.Object, _tollFeeSchedule.Object);
        }

        [Fact]
        public void Vehicle_IsTollFree_ShouldReturnZeroFee()
        {
            Vehicle vehicle = new Motorbike();
            DateTime[] dates = { DateTime.Today.AddHours(7) };

            _tollFeeSchedule.Setup(x => x.CalculateTollFee(It.IsAny<DateTime>())).Returns(0);

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(0, fee);
        }

        [Fact]
        public void TollFreeDate_ShouldReturnZeroFee()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = { new DateTime(2024, 1, 1, 7, 0, 0) };

            _tollFreeDate.Setup(x => x.IsTollFreeDate(It.IsAny<DateTime>())).Returns(true);

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(0, fee);
        }

        [Fact]
        public void SinglePassage_ShouldReturnCorrectFee()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = { DateTime.Today.AddHours(7) };

            _tollFeeSchedule.Setup(x => x.CalculateTollFee(It.IsAny<DateTime>())).Returns(18);

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(18, fee);
        }

        [Fact]
        public void MultiplePassagesWithinOneHour_ShouldReturnHighestFee()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = {
                DateTime.Today.AddHours(6).AddMinutes(20), 
                DateTime.Today.AddHours(6).AddMinutes(45)  
            };

            _tollFeeSchedule.Setup(x => x.CalculateTollFee(It.IsAny<DateTime>())).Returns(13);

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(13, fee);
        }

        [Fact]
        public void MultiplePassagesOverOneHour_ShouldReturnSumOfFees()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = {
                DateTime.Today.AddHours(6).AddMinutes(20), 
                DateTime.Today.AddHours(7).AddMinutes(30) 
            };

            _tollFeeSchedule.Setup(x => x.CalculateTollFee(It.IsAny<DateTime>())).Returns(13);

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(26, fee);
        }

        [Fact]
        public void DailyFee_ShouldNotExceedMaxFeePerDay()
        {
            const int maxFeePerDay = 60;
            Vehicle vehicle = new Car();
            DateTime[] dates = {
                DateTime.Today.AddHours(6).AddMinutes(0), 
                DateTime.Today.AddHours(7).AddMinutes(0),  
                DateTime.Today.AddHours(8).AddMinutes(0),  
                DateTime.Today.AddHours(15).AddMinutes(0), 
                DateTime.Today.AddHours(15).AddMinutes(30), 
                DateTime.Today.AddHours(17).AddMinutes(0)  
            };

            _tollFeeSchedule.Setup(x => x.CalculateTollFee(It.IsAny<DateTime>())).Returns(15);

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(maxFeePerDay, fee);
        }
    }
}