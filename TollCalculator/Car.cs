﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Car : Vehicle
    {
        public string VehicleType { get; } = "Car";
        public bool IsTollFree { get; } = false;
    }
}