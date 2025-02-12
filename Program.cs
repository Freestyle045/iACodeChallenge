using System;
using System.Collections.Generic;
using System.Linq;


class Program
{
    static int ManhattanDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    static List<(CentralFill, double, int)> FindClosestFacilities(int userX, int userY, List<CentralFill> facilities)
    {
        return facilities.OrderBy(f => ManhattanDistance(userX, userY, f.X, f.Y))
                         .Take(3)
                         .Select(f => (f, f.Inventory.Values.Min(), ManhattanDistance(userX, userY, f.X, f.Y)))
                         .ToList();
    }

    static void Main()
    {
        List<CentralFill> facilities = new List<CentralFill>();
        HashSet<(int, int)> usedCoords = new HashSet<(int, int)>();
        Random random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            int x, y;
            do
            {
                x = random.Next(-10, 11);
                y = random.Next(-10, 11);
            } while (usedCoords.Contains((x, y)));

            usedCoords.Add((x, y));
            facilities.Add(new CentralFill(i, x, y));
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

            string[] coordinates = input.Split(',');
            if (coordinates.Length != 2 || !int.TryParse(coordinates[0].Trim(), out int userX) || !int.TryParse(coordinates[1].Trim(), out int userY))
            {
                Console.WriteLine("Invalid input. Please enter coordinates in the format x,y.");
                continue;
            }

            var closestFacilities = FindClosestFacilities(userX, userY, facilities);
            Console.WriteLine($"Closest Central Fills to ({userX},{userY}):");
            foreach (var (facility, cheapestPrice, distance) in closestFacilities)
            {
                Console.WriteLine($"Central Fill {facility.Id:D3} - ${cheapestPrice:F2}, Medication {facility.Inventory.First(kv => kv.Value == cheapestPrice).Key}, Distance {distance}");
            }
        }
    }
}
