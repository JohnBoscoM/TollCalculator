using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class TollFreeDate : ITollFreeDate
    {

        private static readonly List<DateTime> TollFreeDates = new List<DateTime>
    {
        new DateTime(2024, 1, 1),
        new DateTime(2024, 3, 28),
        new DateTime(2013, 3, 29),
        new DateTime(2024, 4, 1),
        new DateTime(2024, 4, 30),
        new DateTime(2024, 5, 1),
        new DateTime(2024, 5, 8),
        new DateTime(2024, 5, 9),
        new DateTime(2024, 6, 5),
        new DateTime(2024, 6, 6),
        new DateTime(2024, 6, 21),
        new DateTime(2024, 11, 1),
        new DateTime(2024, 12, 24),
        new DateTime(2024, 12, 25),
        new DateTime(2024, 12, 26),
        new DateTime(2024, 12, 31)

    };
        public bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || TollFreeDates.Contains(date.Date);
        }
    }
}
