using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Shared.Configuration;
using Shared.Definitions;
using System;
using System.IO;

namespace StaticFilesIO
{
    public static class DefinitionsReader
    {
        public static TStaticDefinitions Import()
        {
            return new TStaticDefinitions()
            {
                Biomes = ReadDefinition<TBiome>(ConfigurationInstance.Config.Files.BiomesFile),
                Resources = ReadDefinition<TResource>(ConfigurationInstance.Config.Files.ResourcesFile),
                Raws = ReadDefinition<TRaw>(ConfigurationInstance.Config.Files.RawsFile)
            };
        }

        private static TDefinitionSet<T> ReadDefinition<T>(string baseFileName) where T : AbstractDefinition
        {
            JSchema schema = ReadSchema(baseFileName);
            return ReadDefinitionBody<T>(baseFileName, schema);
        }

        private static JSchema ReadSchema(string baseFileName)
        {
            string filePath = Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Schemas, baseFileName + ConfigurationInstance.Config.Files.DefinitionsSchemaSuffix + ".json");
            using (TextReader reader = File.OpenText(filePath))
            {
                return JSchema.Load(new JsonTextReader(reader), new JSchemaReaderSettings()
                {
                    Resolver = new JSchemaUrlResolver(),
                    ResolveSchemaReferences = true,
                    BaseUri = new Uri(Path.GetFullPath(filePath))
                });
            }
        }

        private static TDefinitionSet<T> ReadDefinitionBody<T>(string baseFileName, JSchema schema) where T : AbstractDefinition
        {
            using (TextReader r = File.OpenText(Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Definitions, baseFileName + ".json")))
            {
                var jsonValidatingReader = new JSchemaValidatingReader(new JsonTextReader(r))
                {
                    Schema = schema
                };
                var serializer = new JsonSerializer();
                return serializer.Deserialize<TDefinitionSet<T>>(jsonValidatingReader);
            }
        }
    }
}
