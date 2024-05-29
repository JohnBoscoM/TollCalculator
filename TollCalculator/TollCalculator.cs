using System;
using System.Globalization;
using TollFeeCalculator;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */


    private static readonly SortedDictionary<TimeSpan, int> TollFeeSchedule = new SortedDictionary<TimeSpan, int>
{
    { new TimeSpan(6, 0, 0), 8 },
    { new TimeSpan(6, 30, 0), 13 },
    { new TimeSpan(7, 0, 0), 18 },
    { new TimeSpan(8, 0, 0), 13 },
    { new TimeSpan(8, 30, 0), 8 },
    { new TimeSpan(15, 0, 0), 13 },
    { new TimeSpan(15, 30, 0), 18 },
    { new TimeSpan(17, 0, 0), 13 },
    { new TimeSpan(18, 0, 0), 8 }
};
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

    public int CalculateTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || vehicle.IsTollFree) return 0;

        var time = date.TimeOfDay;

        var fee = TollFeeSchedule.LastOrDefault(x => time >= x.Key).Value;

        return fee;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
}