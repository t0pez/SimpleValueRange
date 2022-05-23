using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace SimpleValueRange.Tests.TestData
{
    public class RangeConstructorWithOptionalParamsDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new[]
            {
                new ExampleClass { Property = 10 },
                null
            };
            
            yield return new[]
            {
                null,
                new ExampleClass { Property = 30 }
            };
        }
    }
}