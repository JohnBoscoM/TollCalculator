using System;
using System.Globalization;
using TollFeeCalculator;

namespace TollFeeCalculator
{

    public class TollCalculator
    {
        private readonly ITollFreeDate _tollFreeDate;
        private readonly ITollFeeSchedule _tollFeeSchedule;

        public TollCalculator(ITollFreeDate tollFreeDate, ITollFeeSchedule tollFeeSchedule)
        {
            _tollFreeDate = tollFreeDate;
            _tollFeeSchedule = tollFeeSchedule;
        }

        public int GetTollFee(Vehicle vehicle, DateTime[] dates)
        {
            const int maxMinutesBetween = 60;
            if (vehicle.IsTollFree)
            {
                return 0;
            }

            DateTime intervalStart = dates[0];
            int totalFee = 0;
            int maxFeeInInterval = 0;

            foreach (var date in dates)
            {
                if (_tollFreeDate.IsTollFreeDate(date))
                { 
                    return 0;
                }
                    int nextFee = CalculateTollFee(date, vehicle);
                    double minutesBetween = (date - intervalStart).TotalMinutes;

                    if (minutesBetween <= maxMinutesBetween)
                    {
                        maxFeeInInterval = Math.Max(maxFeeInInterval, nextFee);
                    }
                    else
                    {
                        totalFee += maxFeeInInterval;
                        maxFeeInInterval = nextFee;
                        intervalStart = date;
                    }
                
            }

            totalFee += maxFeeInInterval;
            return Math.Min(totalFee, 60);
        }

        private int CalculateTollFee(DateTime date, Vehicle vehicle)
        {          
            return _tollFeeSchedule.CalculateTollFee(date);
        }
    }
}