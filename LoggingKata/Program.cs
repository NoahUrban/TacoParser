using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            //logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                logger.LogError("Your file is empty");
            }
            else if (lines.Length == 1)
            {
                logger.LogError("Your file onle has 1 line");
            }

            //logger.LogInfo($"Lines: {lines[0]}"); 

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBellA = null;
            ITrackable tacoBellB = null;

            double distance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                var corA = new GeoCoordinate(locations[i].Location.Latitude, locations[i].Location.Longitude);

                for (int j = 0; j < locations.Length; j++)
                {
                    var corB = new GeoCoordinate(locations[j].Location.Latitude, locations[j].Location.Longitude);

                    var corDistance = corA.GetDistanceTo(corB);

                    if (corDistance > distance)
                    {
                        distance = corDistance;
                        tacoBellA = locations[i];
                        tacoBellB = locations[j];
                    }
                }
            }

            Console.WriteLine($"{tacoBellA.Name} Coordinates: ({tacoBellA.Location.Latitude},{tacoBellA.Location.Longitude})");
            Console.WriteLine($"{tacoBellB.Name} Coordinates: ({tacoBellB.Location.Latitude},{tacoBellB.Location.Longitude})");
            Console.WriteLine($"The distance between the two Taco Bell's is {distance}");

        }
    }
}
