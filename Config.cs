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
}