using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    // I use properties in interface for more simplicity and clarity. Methods normally describes "what something does"
    // and properties "what something is"
    // also good practice for a case where data binding is needed
    public interface Vehicle
    {
        string VehicleType { get; }
        bool IsTollFree { get; }
    }
}