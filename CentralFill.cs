using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CentralFill
{
    public int Id { get; }
    public int X { get; }
    public int Y { get; }
    public Dictionary<string, double> Inventory { get; }
    private static Random random = new Random();
    private static string[] medications = { "A", "B", "C" };

    public CentralFill(int id, int x, int y)
    {
        Id = id;
        X = x;
        Y = y;

        // Assign fixed medications with random prices
        Inventory = medications.ToDictionary(med => med, med => Math.Round(random.NextDouble() * 99.99 + 0.01, 2));
    }

    public override string ToString()
    {
        return $"Facility {Id} @ ({X}, {Y}) | Lowest Price: ${Inventory.Values.Min():F2}";
    }
}
