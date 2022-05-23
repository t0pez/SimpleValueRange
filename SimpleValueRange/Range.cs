using System;
using LanguageExt;

namespace SimpleValueRange
{
    /// <summary>
    /// Represents range, that has minimal and maximal values
    /// </summary>
    /// <remarks>
    /// Range is allowed to have both values optional
    /// </remarks>
    /// <typeparam name="T">Range values type</typeparam>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the Range class
        /// </summary>
        /// <param name="min">Minimal range value</param>
        /// <param name="max">Maximal range value</param>
        /// <typeparam name="T">Type of range values</typeparam>
        /// <exception cref="ArgumentException">Throws when minimal value is greater than maximal</exception>
        /// <returns></returns>
        public static Range<T> Create<T>(T min, T max) where T : IComparable<T>
        {
            return new Range<T>(min, max);
        }

        /// <summary>
        /// Initializes a new instance of the Range class
        /// </summary>
        /// <param name="min">Minimal range value</param>
        /// <param name="max">Maximal range value</param>
        /// <typeparam name="T">Type of range values</typeparam>
        /// <exception cref="ArgumentException">Throws when minimal value is greater than maximal</exception>
        /// <returns></returns>
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
            if (min.IsSome && max.IsSome && ((T)min).IsGreaterThan((T)max))
            {
                throw new ArgumentException("Min can't be greater that max");
            }

            _minOption = min;
            _maxOption = max;
        }

        /// <summary>
        /// Determines whether the Range contains it's minimal value
        /// </summary>
        public bool IsMinHasValue => _minOption.IsSome;

        /// <summary>
        /// Determines whether the Range contains it's maximal value
        /// </summary>
        public bool IsMaxHasValue => _maxOption.IsSome;

        /// <summary>
        /// Gets minimal range value 
        /// </summary>
        /// <param name="result"> When this method returns, contains the minimal range value if it isn't optional. Otherwise, the default value for the type of the value parameter</param>
        /// <returns>True if minimal value is not optional. Otherwise returns false</returns>
        public bool TryGetMinValue(out T result)
        {
            return TryGetValue(_minOption, out result);
        }

        /// <summary>
        /// Gets maximal range value 
        /// </summary>
        /// <param name="result">
        /// When this method returns, contains the maximal range value if it isn't optional.
        /// Otherwise, the default value for the type of the value parameter</param>
        /// <returns>True if maximal value is not optional. Otherwise returns false</returns>
        public bool TryGetMaxValue(out T result)
        {
            return TryGetValue(_maxOption, out result);
        }

        /// <summary>
        /// Determines whether the Range contains a specific value. Ignores optional values 
        /// </summary>
        /// <remarks>
        /// If both values are optional, returns true
        /// </remarks>
        /// <param name="element"></param>
        /// <returns>True if the Range contains value. Otherwise returns false</returns>
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