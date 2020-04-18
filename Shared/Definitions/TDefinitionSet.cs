namespace Shared.Definitions
{
    public class TDefinitionSet<T> where T : AbstractDefinition
    {
        public T[] Items { get; set; }
    }
}
