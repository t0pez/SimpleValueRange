using System;
using LanguageExt;

namespace SimpleValueRange
{
    public class Range<T> where T : IComparable<T>
    {
        public static Range<T> Create<T>(T min, T max) where T : IComparable<T>
        {
            if (min != null && max != null && min.IsGreaterThan(max))
            {
                throw new ArgumentException("Min can't be greater that max");
            }
            
            return new Range<T>(min, max);
        }

        public static Range<T> Create<T>(T? min, T? max) where T : struct, IComparable<T>
        {
            if (min != null && max != null && ((T)min).IsGreaterThan((T)max))
            {
                throw new ArgumentException("Min can't be greater that max");
            }
            
            var minOption = min ?? Option<T>.None;
            var maxOption = max ?? Option<T>.None;

            return new Range<T>(minOption, maxOption);
        }
        
        private readonly Option<T> _minOption;
        private readonly Option<T> _maxOption;

        private Range(Option<T> min, Option<T> max)
        {
            _minOption = min;
            _maxOption = max;
        }

        public bool MinHasValue => _minOption.IsSome;
        public bool MaxHasValue => _maxOption.IsSome;
        
        public bool TryGetMinValue(out T result)
        {
            if (_minOption.IsNone)
            {
                result = default;
                return false;
            }

            result = (T)_minOption.Case;
            return true;
        }
        
        public bool TryGetMaxValue(out T result)
        {
            if (_maxOption.IsNone)
            {
                result = default;
                return false;
            }

            result = (T)_maxOption.Case;
            return true;
        }
            
        public bool Contains(T element)
        {
            return ElementGreaterThatMin(element) && ElementLessThanMax(element);
        }

        private bool ElementLessThanMax(T element)
        {
            return element.IsLessThan(_maxOption) || element.IsEqualTo(_maxOption);
        }

        private bool ElementGreaterThatMin(T element)
        {
            return element.IsGreaterThan(_minOption) || element.IsEqualTo(_minOption);
        }
    }
}