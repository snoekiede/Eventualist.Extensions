# Eventualist.Extensions
This package contains some small but handy extension methods


A number of simple extensions to bool, and collections. Mainly used for my own website, but you can peruse for your own pleasure.
* Bool: AddNot (transforms a string according to the value of the bool)
* Bool: ToYesOrNo (transforms bool to a yes or no)
* Collection: IsEmpty: returns true if collection is empty
* Collection: IsNotEmpty: returns true if collection is not empty
* Collection Divide: returns a list of sublists of the collections, with a specified maximumlength

## In 1.0.0.13

- Memoize, to automatically cache function results. Just apply Memoize() to a Function object to get a memoized version. Caveats: it only works for up to two arguments, and it does not much benefit recursive functions.

## In 2.0.0.0

- No new functionality but now compatible with .net 6.0. For compatibility with older versions use 1.0.0.19</Description>
    
## In 2.0.0.9

- Added new functionality for Memoize: up to six arguments are now supported

## In 2.0.0.15

- Updated to .NET 7. Added extra unit tests and null safety checks

## In 3.0.0.9

- Update to .NET 8

## In 3.0.0.17
- Added the `MustComeBefore` attribute to make sure dates in your model are always in the right order.

## In 4.0.0.0
- Updated to .NET 9.0
- Added the `MustComeBefore` attribute to make sure dates in your model are always in the right order. This is useful for validating that a start date comes before an end date, for example.
- Updated the `ExtendedDictionary` to make it more performant and thread-safe.
- The `ConvertToMimeType` now handles a greater range of possibilities.
- The `HasCorrectExtension` method has been improved to handle more file types and extensions.
- The `Titleize` method has been improved to handle more edge cases and provide better title formatting.
- So has the `Abbreviate` method, which now handles more complex cases and provides better abbreviation results.
- The `Truncate` has been added which does not respect word boundaries, allowing for more precise truncation of strings.
- A `StripHtml` method has been added to remove HTML tags from strings, making it easier to work with plain text.
- Plus three small utility methods.


An example use of this would be:
```
    internal class TimePeriod
    {
        [MustComeBefore("EndDate")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
```

This attribute works with the standard .NET validators.