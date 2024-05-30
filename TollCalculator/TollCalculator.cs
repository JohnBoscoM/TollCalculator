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
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = CalculateTollFee(date, vehicle);
                int tempFee = CalculateTollFee(intervalStart, vehicle);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private int CalculateTollFee(DateTime date, Vehicle vehicle)
        {
            if(_tollFreeDate.IsTollFreeDate(date) || vehicle.IsTollFree)

                return 0;
           
            return _tollFeeSchedule.CalculateTollFee(date);
        }


    }
}