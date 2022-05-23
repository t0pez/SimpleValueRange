using System;

namespace SimpleValueRange.Tests
{
    public class ExampleClass : IComparable<ExampleClass>
    {
        public decimal Property { get; set; }

        public int CompareTo(ExampleClass other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            
            return Property.CompareTo(other.Property);
        }
    }
}