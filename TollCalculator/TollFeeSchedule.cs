using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator;

namespace TollFeeCalculator
{
    public class TollFeeSchedule: ITollFeeSchedule
    {
        private static readonly SortedDictionary<TimeSpan, int> FeeSchedule = new SortedDictionary<TimeSpan, int>
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

        public int CalculateTollFee(DateTime date)
        {

            var time = date.TimeOfDay;

            var fee = FeeSchedule.LastOrDefault(x => time >= x.Key).Value;

            return fee;
        }
    }
}
