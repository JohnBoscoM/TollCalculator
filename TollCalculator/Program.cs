// See https://aka.ms/new-console-template for more information
using TollFeeCalculator;

TollCalculator tollCalculator = new TollCalculator();

DateTime[] dates = new DateTime[] { DateTime.Today.AddHours(17) };
Vehicle vehicle = new Car();
int fee = tollCalculator.GetTollFee(vehicle, dates);

// Print the result
Console.WriteLine($"The toll fee is: {fee}");
