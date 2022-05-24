# SimpleValueRange

This project is a .NET Standard 2.0 Class Library

This [NuGet Package](https://www.nuget.org/packages/tapa_.SimpleValueRange/) provides a class Range, that represents range of generic type which both values can be optional

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
In this case method `Contains` will return `true` for `any` value

```c#
int? min = null;
int? max = null;
var range = Range<int>.Create(min, max);

range.Contains(5); // true
```

If you need to have only one optional value in range, you can do it.
In this case method `Contains` will compare values by only one `active` border of range

```c#
int? min = null;
int max = 5;
var range = Range<int>.Create(min, max);

range.Contains(1); // true
range.Contains(6); // false
```

Range can also accept classes too.

```c#
public class MyClass : IComparable<MyClass>
{
  // Some code
}

var min = new MyClass(...);
var max = new MyClass(...);
var range = Range<MyClass>.Create(min, max);
```



