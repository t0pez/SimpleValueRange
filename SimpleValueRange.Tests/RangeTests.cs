using System;
using SimpleValueRange.Tests.TestData;
using SimpleValueRange.Tests.TestData.SimpleValueRange.Tests;
using Xunit;

namespace SimpleValueRange.Tests
{
    public class RangeTests
    {
        [Theory]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        public void RangeConstructor_CorrectValues_WorksFine(int min, int max)
        {
            var range = Range<int>.Create(min, max);

            range.TryGetMinValue(out var minValue);
            range.TryGetMaxValue(out var maxValue);

            Assert.Equal(min, minValue);
            Assert.Equal(max, maxValue);
        }

        [Theory]
        [InlineData(null, 3)]
        [InlineData(2, null)]
        public void RangeConstructor_HasOptionalValues_WorksFine(int? min, int? max)
        {
            int? GetMinValue(Range<int> range)
            {
                int? result;
                if (range.TryGetMinValue(out var value))
                {
                    result = value;
                }
                else
                {
                    result = null;
                }

                return result;
            }

            int? GetMaxValue(Range<int> range)
            {
                int? result;
                if (range.TryGetMaxValue(out var value))
                {
                    result = value;
                }
                else
                {
                    result = null;
                }

                return result;
            }

            var range = Range<int>.Create(min, max);

            var minValue = GetMinValue(range);
            var maxValue = GetMaxValue(range);

            Assert.Equal(min, minValue);
            Assert.Equal(max, maxValue);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(4, 2)]
        [InlineData(3, 2)]
        public void RangeConstructor_MinGreaterThanMax_ThrowsException(int min, int max)
        {
            var constructorMethod = new Action(() => { Range<int>.Create(min, max); });

            Assert.Throws<ArgumentException>(constructorMethod);
        }

        [Theory]
        [InlineData(1, 3, 2)]
        [InlineData(1, 4, 3)]
        public void RangeContains_ElementBetweenMinAndMax_ReturnsTrue(int min, int max, int element)
        {
            var range = Range<int>.Create(min, max);

            var actualResult = range.Contains(element);

            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(1, 3, 1)]
        [InlineData(1, 4, 4)]
        public void RangeContains_ElementIsEqualMinOrMax_ReturnsTrue(int min, int max, int element)
        {
            var range = Range<int>.Create(min, max);

            var actualResult = range.Contains(element);

            Assert.True(actualResult);
        }
        
        [Theory]
        [InlineData(1, null, 1)]
        [InlineData(1, null, 2)]
        [InlineData(null, 4, 2)]
        [InlineData(null, 4, 4)]
        public void RangeContains_ElementPassesOptionalBorder_ReturnsTrue(int? min, int? max, int element)
        {
            var range = Range<int>.Create(min, max);

            var actualResult = range.Contains(element);

            Assert.True(actualResult);
        }

        [Theory]
        [InlineData(1, 3, 4)]
        [InlineData(2, 4, 1)]
        public void RangeContains_ElementMoreThanMaxOrLessThanMin_ReturnsFalse(int min, int max, int element)
        {
            var range = Range<int>.Create(min, max);

            var actualResult = range.Contains(element);

            Assert.False(actualResult);
        }
        
        [Theory]
        [RangeConstructorWithoutOptionalParamsData]
        public void RangeConstructor_ClassWithoutOptionalValues_WorksFine(ExampleClass min, ExampleClass max)
        {
            var range = Range<ExampleClass>.Create(min, max);

            range.TryGetMinValue(out var minValue);
            range.TryGetMaxValue(out var maxValue);

            Assert.Equal(min, minValue);
            Assert.Equal(max, maxValue);
        }

        [Theory]
        [RangeConstructorWithOptionalParamsData]
        public void RangeConstructor_ClassWithOptionalValues_WorksFine(ExampleClass min, ExampleClass max)
        {
            var range = Range<ExampleClass>.Create(min, max);

            range.TryGetMinValue(out var minValue);
            range.TryGetMaxValue(out var maxValue);

            Assert.Equal(min, minValue);
            Assert.Equal(max, maxValue);
        }

        [Theory]
        [RangeContainsData]
        public void RangeContains_ClassElementIsEqualMinOrMax_ReturnsTrue(
            ExampleClass min, ExampleClass max, ExampleClass element)
        {
            var range = Range<ExampleClass>.Create(min, max);

            var actualResult = range.Contains(element);

            Assert.True(actualResult);
        }

        [Theory]
        [RangeNotContainsData]
        public void RangeContains_ClassElementMoreThanMaxOrLessThanMin_ReturnsFalse(
            ExampleClass min, ExampleClass max, ExampleClass element)
        {
            var range = Range<ExampleClass>.Create(min, max);

            var actualResult = range.Contains(element);

            Assert.False(actualResult);
        }
    }
}