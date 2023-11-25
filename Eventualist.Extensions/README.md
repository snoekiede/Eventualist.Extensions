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