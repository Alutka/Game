using Newtonsoft.Json;
using Shared.Structures;
using System.Collections.Generic;
using System.IO;

namespace StaticFilesIO
{
    public class DefinitionsReader
    {
        private string _definitionsDirectory;

        public DefinitionsReader(string definitionsDirectoryPath)
        {
            _definitionsDirectory = definitionsDirectoryPath;
        }

        public Dictionary<string, TDefinition> ReadDefinitions()
        {
            var result = new Dictionary<string, TDefinition>();
            IEnumerable<string> definitions = Directory.GetFiles(_definitionsDirectory);
            foreach (var definition in definitions)
            {
                using (StreamReader r = new StreamReader(Path.Combine(_definitionsDirectory, definition)))
                {
                    string json = r.ReadToEnd();
                    var jsonResult = JsonConvert.DeserializeObject<string[]>(json);
                    result.Add(Path.GetFileNameWithoutExtension(definition), new TDefinition(jsonResult));
                }
            }
            return result;
        }
    }
}
