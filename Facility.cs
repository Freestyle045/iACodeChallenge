using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralFill
{
    class Facility
    {
        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public Dictionary<string, double> Inventory { get; }
        private static Random random = new Random();

        public Facility(int id, int x, int y, Config config)
        {
            Id = id;
            X = x;
            Y = y;

            // When a Facility is created, assign random prices to the medications based on price limits in config
            Inventory = config.Medications.ToDictionary(
                med => med, med => Math.Round(random.NextDouble() * (config.PriceMax - config.PriceMin) + config.PriceMin, 2)
            );
        }

        public override string ToString()
        {
            return $"Facility {Id} @ ({X}, {Y}) | Inventory: {string.Join(", ", Inventory.Select(kv => $"{kv.Key}: ${kv.Value:F2}"))}";
        }
    }
}
