using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Shared.Configuration;
using Shared.Definitions;
using System.IO;

namespace StaticFilesIO
{
    public static class DefinitionsReader
    {
        public static TStaticDefinitions Import()
        {
            return new TStaticDefinitions()
            {
                Biomes = ReadDefinition<TDefinitionSet<TBiome>>(ConfigurationInstance.Config.Files.BiomesFile),
                Resources = ReadDefinition<TDefinitionSet<TResource>>(ConfigurationInstance.Config.Files.ResourcesFile)
            };
        }

        private static T ReadDefinition<T>(string baseFileName) where T : AbstractDefinitionSet
        {
            JSchema schema = ReadSchema(baseFileName);
            return ReadDefinitionBody<T>(baseFileName, schema);
        }

        private static JSchema ReadSchema(string baseFileName)
        {
            using (TextReader reader = File.OpenText(Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Schemas, baseFileName + ConfigurationInstance.Config.Files.DefinitionsSchemaSuffix + ".json")))
            {
                return JSchema.Load(new JsonTextReader(reader));
            }
        }

        private static T ReadDefinitionBody<T>(string baseFileName, JSchema schema) where T : AbstractDefinitionSet
        {
            using (TextReader r = File.OpenText(Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Definitions, baseFileName + ".json")))
            {
                var jsonValidatingReader = new JSchemaValidatingReader(new JsonTextReader(r))
                {
                    Schema = schema
                };
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(jsonValidatingReader);
            }
        }
    }
}
