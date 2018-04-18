using System;
using MoreLinq;

namespace sumHours
{
    class Program
    {
        static void Main(string[] args)
        {
            var positiveHours = "00:58:00,00:59:00,00:22:00,00:21:00,00:05:00,02:10:00,00:03:00,00:35:00,01:19:00,01:52:00,"
                              + "03:37:00,01:14:00,02:08:00,00:20:00,01:19:00,00:49:00,05:15:00,03:50:00";

            var homeOffice = "18:00:00";
            var sumPositive = new TimeSpan();
            positiveHours
                .Split(',')
                .ForEach(hour => 
                {
                    var h = TimeSpan.Parse(hour);
                    sumPositive += h;
                });

            sumPositive += TimeSpan.Parse(homeOffice);
            Console.WriteLine($"Total Hours: {sumPositive.TotalHours}");
        }
    }
}
