namespace Neuro.Entity
{
    /// <summary>
    /// reference:
    /// https://github.com/andrewkirillov/AForge.NET/blob/master/Sources/Core/Range.cs
    /// </summary>
    public class Range
    {
        public float Min { get; private set; }

        public float Max { get; private set; }

        public float Length { get { return Max - Min; } }

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool IsInside(float x)
        {
            return ((x >= Min) && (x <= Max));
        }

        public bool IsInside(Range range)
        {
            return ((IsInside(range.Min)) && (IsInside(range.Max)));
        }

        public bool IsOverlapping(Range range)
        {
            return ((IsInside(range.Min)) || (IsInside(range.Max)) ||
                     (range.IsInside(Min)) || (range.IsInside(Max)));
        }

        public static bool operator ==(Range range1, Range range2)
        {
            return ((range1.Min == range2.Min) && (range1.Max == range2.Max));
        }

        public static bool operator !=(Range range1, Range range2)
        {
            return ((range1.Min != range2.Min) || (range1.Max != range2.Max));
        }

        public override bool Equals(object obj)
        {
            return (obj is Range) ? (this == (Range)obj) : false;
        }

        public override int GetHashCode()
        {
            return Min.GetHashCode() + Max.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}, {1}", Min, Max);
        }

    }
}
