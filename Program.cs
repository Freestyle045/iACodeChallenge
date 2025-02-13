using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CentralFill
{

    class Config
    {
        public int GridSize { get; set; }
        public int FacilityCount { get; set; }
        public List<string> Medications { get; set; }
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public bool DebugMode { get; set; }

        //Read the json file and store the values to the variables in the config object
        public static Config LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Config file not found: {filePath}");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }

    class Program
    {
        static int ManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        // Input: user-entered coordinates and a list of facilities.
        // Return: A list containing only the three facilities with the lowest ManhattanDistance.
        // Lowest mediction cost and ManhattanDistance are returned along with each facility in the list.
        static List<(Facility, double, int)> FindClosestFacilities(int userX, int userY, List<Facility> facilities)
        {
            return facilities.OrderBy(f => ManhattanDistance(userX, userY, f.X, f.Y))
                             .Take(3)
                             .Select(f => (f, f.Inventory.Values.Min(), ManhattanDistance(userX, userY, f.X, f.Y)))
                             .ToList();
        }

        static void Main()
        {
            // Load configuration
            Config config = Config.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));

            List<Facility> facilities = new List<Facility>();
            HashSet<(int, int)> usedCoords = new HashSet<(int, int)>();
            Random random = new Random();

            // Generate facilities based on config
            for (int i = 1; i <= config.FacilityCount; i++)
            {
                // Ensure that locations don't get used more than once
                int x, y;
                do
                {
                    x = random.Next(-config.GridSize, config.GridSize + 1);
                    y = random.Next(-config.GridSize, config.GridSize + 1);
                } while (usedCoords.Contains((x, y)));

                usedCoords.Add((x, y));
                facilities.Add(new Facility(i, x, y, config));
            }

            // Debug mode can be turned on from the config file
            if (config.DebugMode)
            {
                Console.WriteLine("[DEBUG] Here is the seeded data:");
                foreach (var facility in facilities)
                {
                    Console.WriteLine(facility);
                }
                Console.WriteLine();
            }

            Console.Write("Welcome to the Central Fill System.\n" +
                          "Please Input Coordinates: x,y\n" +
                          "Type 'exit' to quit.\n");

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine().Trim();
                if (input.ToLower() == "exit")
                    break;

                // Read and Validate Input
                string[] coordinates = input.Split(',');
                if (coordinates.Length != 2 || !int.TryParse(coordinates[0].Trim(), out int userX) || !int.TryParse(coordinates[1].Trim(), out int userY))
                {
                    Console.WriteLine("Invalid input. Please enter coordinates in the format x,y.");
                    continue;
                }

                // Output the Results of the search
                var closestFacilities = FindClosestFacilities(userX, userY, facilities);
                Console.WriteLine($"Closest Central Fills to ({userX},{userY}):");
                foreach (var (facility, cheapestPrice, distance) in closestFacilities)
                {
                    Console.WriteLine($"Central Fill {facility.Id:D3} - ${cheapestPrice:F2}, Medication {facility.Inventory.First(kv => kv.Value == cheapestPrice).Key}, Distance {distance}");
                }
            }
        }
    }
}