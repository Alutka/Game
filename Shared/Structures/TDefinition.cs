using System;

namespace Shared.Structures
{
    public class TDefinition
    {
        private readonly string _name;
        private readonly string[] _values;

        public TDefinition(string name, string[] values)
        {
            _values = values;
            _name = name;
        }

        public int GetKey(string value)
        {
            var result = Array.IndexOf(_values, value);
            if (result < 0) throw new InvalidCastException($"{_name} {value} does not exist!");
            return result;
        }

        public string GetValue(int key) => _values[key];
    }
}
