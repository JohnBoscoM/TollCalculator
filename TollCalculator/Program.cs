// See https://aka.ms/new-console-template for more information
using TollFeeCalculator;

TollCalculator tollCalculator = new TollCalculator();

DateTime date = DateTime.Now; 
Vehicle vehicle = new Car(); 
int fee = tollCalculator.GetTollFee(date, vehicle);

// Print the result
Console.WriteLine($"The toll fee is: {fee}");
