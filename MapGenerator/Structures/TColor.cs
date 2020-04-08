namespace MapGenerator.Structures
{
    public class TColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public override int GetHashCode()
        {
            return R + G * 1000 + B * 1000000;
        }

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }
    }
}
