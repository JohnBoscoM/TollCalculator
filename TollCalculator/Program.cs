// See https://aka.ms/new-console-template for more information
using TollFeeCalculator;

ITollFeeSchedule tollFeeSchedule = new TollFeeSchedule();
ITollFreeDate tollFreeDate = new TollFreeDate();

TollCalculator tollCalculator = new TollCalculator(tollFreeDate,tollFeeSchedule);

DateTime[] dates = new DateTime[] {new DateTime(year: 2022, month: 12, day: 24).AddHours(16) };
Vehicle vehicle = new Car();
int fee = tollCalculator.GetTollFee(vehicle, dates);

// Print the result
Console.WriteLine($"The toll fee is: {fee}");
