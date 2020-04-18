namespace Shared.Map
{
    public class TLayerEnum
    {
        private string[] _enum;

        public int Length => _enum.Length;

        public TLayerEnum(string[] names)
        {
            _enum = names;
        }

        public string GetName(int value)
        {
            return _enum[value];
        }
    }
}
