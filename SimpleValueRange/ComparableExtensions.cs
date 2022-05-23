using System;
using LanguageExt;

namespace SimpleValueRange
{
    internal static class ComparableExtensions
    {
        public static bool IsGreaterThan<T>(this T element, T other) where T : IComparable<T>
        {
            return element.CompareTo(other) == 1;
        }

        public static bool IsGreaterThan<T>(this T element, Option<T> other) where T : IComparable<T>
        {
            if (other.IsNone)
                return true;

            var value = other.Case;
            return element.CompareTo((T)value) == 1;
        }

        public static bool IsLessThan<T>(this T element, T other) where T : IComparable<T>
        {
            return element.CompareTo(other) == -1;
        }
        
        public static bool IsLessThan<T>(this T element, Option<T> other) where T : IComparable<T>
        {
            if (other.IsNone)
                return true;

            var value = other.Case;
            return element.CompareTo((T)value) == -1;
        }

        public static bool IsEqualTo<T>(this T element, T other) where T : IComparable<T>
        {
            return element.CompareTo(other) == 0;
        }
        
        public static bool IsEqualTo<T>(this T element, Option<T> other) where T : IComparable<T>
        {
            if (other.IsNone)
                return true;

            var value = other.Case;
            return element.CompareTo((T)value) == 0;
        }
    }
}