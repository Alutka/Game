using System;

namespace Shared.Structures
{
    public class TEnum
    {
        private readonly string[] _values;

        public int Length => _values.Length;

        public TEnum(string[] values)
        {
            _values = values;
        }

        public int GetKey(string value)
        {
            return Array.IndexOf(_values, value);
        }

        public string GetValue(int key) => _values[key];
    }
}
