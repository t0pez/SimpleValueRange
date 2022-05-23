﻿using System;
using LanguageExt;

namespace SimpleValueRange
{
    public class Range<T> where T : IComparable<T>
    {
        public static Range<T> Create<T>(T min, T max) where T : IComparable<T>
        {
            return new Range<T>(min, max);
        }

        public static Range<T> Create<T>(T? min, T? max) where T : struct, IComparable<T>
        {
            var minOption = min ?? Option<T>.None;
            var maxOption = max ?? Option<T>.None;

            return new Range<T>(minOption, maxOption);
        }

        private readonly Option<T> _minOption;
        private readonly Option<T> _maxOption;

        private Range(Option<T> min, Option<T> max)
        {
            if (min.IsNone && max.IsNone)
            {
                throw new ArgumentException("Both values can't be optional");
            }

            if (min.IsSome && max.IsSome && ((T)min).IsGreaterThan((T)max))
            {
                throw new ArgumentException("Min can't be greater that max");
            }

            _minOption = min;
            _maxOption = max;
        }

        public bool MinHasValue => _minOption.IsSome;
        public bool MaxHasValue => _maxOption.IsSome;

        public bool TryGetMinValue(out T result)
        {
            return TryGetValue(_minOption, out result);
        }

        public bool TryGetMaxValue(out T result)
        {
            return TryGetValue(_maxOption, out result);
        }

        public bool Contains(T element)
        {
            return ElementGreaterOrEqualToMin(element) && ElementLessOrEqualToMax(element);
        }

        private bool TryGetValue(Option<T> option, out T result)
        {
            if (option.IsNone)
            {
                result = default;
                return false;
            }

            result = (T)option.Case;
            return true;
        }

        private bool ElementLessOrEqualToMax(T element)
        {
            return element.IsLessThan(_maxOption) || element.IsEqualTo(_maxOption);
        }

        private bool ElementGreaterOrEqualToMin(T element)
        {
            return element.IsGreaterThan(_minOption) || element.IsEqualTo(_minOption);
        }
    }
}