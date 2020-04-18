using System;

namespace Shared.Structures
{
    public class TEnum
    {
        private readonly string[] _values;

        public TEnum(string[] values)
        {
            _values = values;
        }

        public int GetKey(string value)
        {
            var result = Array.IndexOf(_values, value);
            if (result < 0) throw new InvalidCastException($"{value} does not exist!");
            return result;
        }

        public string GetValue(int key) => _values[key];
    }
}
