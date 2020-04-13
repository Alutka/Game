using Newtonsoft.Json;
using Shared;
using Shared.Configuration;
using Shared.Definitions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaticFilesIO
{
    public class DefinitionsReader
    {
        private readonly string _definitionsDirectory;

        public DefinitionsReader()
        {
            _definitionsDirectory = Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Definitions);
        }

        public TStaticDefinitions Import()
        {
            IEnumerable<string> definitions = Directory.GetFiles(_definitionsDirectory).Select(filePath => Path.GetFileName(filePath));
            var result = new List<AbstractDefinitionSet>();
            foreach (var definition in definitions)
            {
                using (StreamReader r = new StreamReader(Path.Combine(_definitionsDirectory, definition)))
                {
                    string json = r.ReadToEnd();
                    result.Add(JsonConvert.DeserializeObject<AbstractDefinitionSet>(json));
                }
            }
            return new TStaticDefinitions()
            {
                Biomes = result.First(def => def.Type == DefinitionType.Biome) as TDefinitionSet<TBiome>
            };
        }
    }
}
