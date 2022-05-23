# SimpleValueRange

This project is a .NET Standard 2.0 Class Library.

This project provides a class Range, that represents range of generic type which both values can be optiona;

# Example

To define new range use static `Create` method

```c#
var range = Range<int>.Create(1, 10);
```


And use method `Contains`, that determines whether the Range contains a value

```c#
var isInRange = range.Contains(5);
```


You can also pass nulls in constructor.
In this case method `Contains` will return `true` for any value

```c#
var range = Range<int>.Create(null, null);

range.Contains(5); // true
```





If you need to have only one optional value in range, you can do it.
In this case method `Contains` will compare values by only one `active` border of range

```c#
var range = new Range<int>(null, 5);

range.Contains(1); // true
range.Contains(6); // false
```








