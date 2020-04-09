namespace Shared.Map
{
    public class TMap
    {
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public TMapLayer[] Layers { get; set; }
    }
}
