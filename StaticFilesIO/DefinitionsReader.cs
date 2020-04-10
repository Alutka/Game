using Newtonsoft.Json;
using Shared.Configuration;
using Shared.Structures;
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

        public Dictionary<string, TDefinition> Import()
        {
            var result = new Dictionary<string, TDefinition>();
            IEnumerable<string> definitions = Directory.GetFiles(_definitionsDirectory).Select(filePath => Path.GetFileName(filePath));
            foreach (var definition in definitions)
            {
                using (StreamReader r = new StreamReader(Path.Combine(_definitionsDirectory, definition)))
                {
                    string json = r.ReadToEnd();
                    var jsonResult = JsonConvert.DeserializeObject<string[]>(json);
                    string definitionName = Path.GetFileNameWithoutExtension(definition);
                    result.Add(definitionName, new TDefinition(definitionName, jsonResult));
                }
            }
            return result;
        }
    }
}
