using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator;

namespace TollFeeCalculator
{
    public interface ITollFeeSchedule
    {
      int CalculateTollFee(DateTime date);
    }
}
