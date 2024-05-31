using TollFeeCalculator;

ITollFeeSchedule tollFeeSchedule = new TollFeeSchedule();
ITollFreeDate tollFreeDate = new TollFreeDate();

TollCalculator tollCalculator = new TollCalculator(tollFreeDate,tollFeeSchedule);

DateTime[] dates =
{
    DateTime.Today.AddHours(6).AddMinutes(20),
    DateTime.Today.AddHours(6).AddMinutes(45),
    DateTime.Today.AddHours(15).AddMinutes(05),
    DateTime.Today.AddHours(15).AddMinutes(40),
    DateTime.Today.AddHours(18),
    DateTime.Today.AddHours(18).AddMinutes(40),
};
Vehicle vehicle = new Car();
int fee = tollCalculator.GetTollFee(vehicle, dates);

Console.WriteLine($"The toll fee is: {fee}");
