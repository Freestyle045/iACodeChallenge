using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralFill
{
    class Config
    {
        public int GridSize { get; set; }
        public int FacilityCount { get; set; }
        public List<string> Medications { get; set; } = new List<string>();
        public double PriceMin { get; set; }
        public double PriceMax { get; set; }
        public bool DebugMode { get; set; }

        // Basic validation for values in the config file
        public void Validate()
        {
            if (GridSize <= 0)
                throw new ArgumentException("GridSize must be greater than zero.");
            if (FacilityCount <= 0)
                throw new ArgumentException("FacilityCount must be greater than zero.");
            if (PriceMin < 0 || PriceMax < 0 || PriceMin > PriceMax)
                throw new ArgumentException("Invalid price range.");
            if (Medications == null || Medications.Count == 0)
                throw new ArgumentException("Medications list cannot be empty.");
        }

        // Read the json file and store the values to the variables in the config object.
        // If the file is in the wrong format or not present, exit gracefully.
        public static Config LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Config file not found: {filePath}");

            try
            {
                string json = File.ReadAllText(filePath);
                var config = JsonConvert.DeserializeObject<Config>(json);

                if (config == null)
                    throw new InvalidOperationException("Failed to parse configuration file.");

                return config;
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Error parsing JSON config file.", ex);
            }
        }
    }
}