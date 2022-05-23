using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace SimpleValueRange.Tests.TestData
{
    public class RangeConstructorWithoutOptionalParamsDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new[]
            {
                new ExampleClass { Property = 10 },
                new ExampleClass { Property = 20 }
            };
            
            yield return new[]
            {
                new ExampleClass { Property = 30 },
                new ExampleClass { Property = 30 }
            };
        }
    }
}