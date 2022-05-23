using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace SimpleValueRange.Tests.TestData
{
    namespace SimpleValueRange.Tests
    {
        public class RangeNotContainsDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                yield return new[]
                {
                    new ExampleClass
                    {
                        Property = 10
                    },
                    new ExampleClass
                    {
                        Property = 30
                    },
                    new ExampleClass
                    {
                        Property = 9
                    }
                };
                yield return new[]
                {
                    new ExampleClass
                    {
                        Property = 15
                    },
                    new ExampleClass
                    {
                        Property = 25
                    },
                    new ExampleClass
                    {
                        Property = 40
                    }
                };
                yield return new[]
                {
                    new ExampleClass
                    {
                        Property = 15
                    },
                    new ExampleClass
                    {
                        Property = 25
                    },
                    new ExampleClass
                    {
                        Property = 30
                    }
                };
            }
        }
    }
}