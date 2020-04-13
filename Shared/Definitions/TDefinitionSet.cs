namespace Shared.Definitions
{
    public class TDefinitionSet<T> : AbstractDefinitionSet where T : AbstractDefinition
    {
        public T[] Items { get; set; }
    }
}
