using System;

namespace Shared.Structures
{
    public class TDefinition
    {
        private readonly string[] _values;

        public TDefinition(string[] values)
        {
            _values = values;
        }

        public int GetKey(string value) => Array.IndexOf(_values, value);

        public string GetValue(int key) => _values[key];
    }
}
