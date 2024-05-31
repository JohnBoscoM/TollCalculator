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

        [Fact]
        public void MultiplePassagesWithinOneHour_ShouldReturnHighestFee()
        {
            Vehicle vehicle = new Car();
            DateTime[] dates = {
                DateTime.Today.AddHours(6).AddMinutes(20), 
                DateTime.Today.AddHours(6).AddMinutes(45)  
            };

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

            int fee = _tollCalculator.GetTollFee(vehicle, dates);

            Assert.Equal(maxFeePerDay, fee);
        }
    }
}
