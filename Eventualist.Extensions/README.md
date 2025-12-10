# Eventualist.Extensions

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A collection of lightweight, focused extension methods for common .NET types. Originally created for personal projects but available for anyone to use.

## ⚠️ Disclaimer

This library is primarily developed for personal use and experimentation. While it is publicly available and contributions are welcome, please note:

- **No Warranty**: This software is provided "as is", without warranty of any kind, express or implied
- **Use at Your Own Risk**: The library may contain bugs or incomplete features
- **Breaking Changes**: API changes may occur between versions without extensive deprecation notices
- **Limited Support**: Support and maintenance are provided on a best-effort basis
- **Experimental Features**: Some features may be experimental, especially those targeting preview versions of .NET

For production use, please thoroughly test the library in your specific context and consider pinning to a specific version.

## Features

### Boolean Extensions
- **`AddNot(string text, string negation = "not ")`** - Conditionally prefixes a string with a negation word based on the boolean value
  ```csharp
  false.AddNot("implemented") // Returns "not implemented"
  true.AddNot("implemented")  // Returns "implemented"
  ```
- **`ToYesOrNo(string yes = "yes", string no = "no", string unknown = "unknown")`** - Converts boolean to yes/no strings (supports nullable booleans)
  ```csharp
  true.ToYesOrNo()   // Returns "yes"
  false.ToYesOrNo()  // Returns "no"
  ((bool?)null).ToYesOrNo() // Returns "unknown"
  ```

### Collection Extensions
- **`IsEmpty<T>()`** - Returns true if the collection is empty
- **`IsNotEmpty<T>()`** - Returns true if the collection contains any elements
- **`Divide<T>(int maxLength)`** - Splits a collection into sublists with a specified maximum length
  ```csharp
  var numbers = new[] { 1, 2, 3, 4, 5, 6, 7 };
  var chunks = numbers.Divide(3); // Returns [[1,2,3], [4,5,6], [7]]
  ```

### String Extensions
- **`Titleize()`** - Converts a string to title case
  ```csharp
  "helloWorld".Titleize() // Returns "Hello World"
  "SHOUTING_TEXT".Titleize() // Returns "Shouting_TEXT"
  "snake_case_example".Titleize() // Returns "Snake_case_example"
  ```
- **`Abbreviate(int maxLength, string abbreviationSymbol = "...")`** - Shortens a string to a maximum length with ellipsis
  ```csharp
  "This is a very long sentence that needs to be abbreviated".Abbreviate(30)
  // Returns "This is a very long..."
  
  "Short text".Abbreviate(20) // Returns "Short text" (no truncation needed)
  ```
- **`Truncate(int maxLength, string suffix = "...")`** - Truncates a string to a maximum length
  ```csharp
  "Hello World".Truncate(8) // Returns "Hello..."
  "Hello World".Truncate(8, "[more]") // Returns "Hel[more]"
  ```
- **`StripHtml()`** - Removes HTML tags from a string
  ```csharp
  "<p>Hello <strong>World</strong></p>".StripHtml() // Returns "Hello World"
  ```
- **`HasCorrectExtension(params string[] additionalExtensions)`** - Validates file extension (defaults to image extensions)
  ```csharp
  "photo.jpg".HasCorrectExtension() // Returns true
  "document.pdf".HasCorrectExtension() // Returns false
  "document.pdf".HasCorrectExtension("pdf", "doc") // Returns true
  ```
- **`ConvertToMimeType()`** - Converts file extension to MIME type
  ```csharp
  "jpg".ConvertToMimeType() // Returns "image/jpeg"
  ".png".ConvertToMimeType() // Returns "image/png"
  "pdf".ConvertToMimeType() // Returns "application/pdf"
  "unknown".ConvertToMimeType() // Returns "application/octet-stream"
  ```
- **`ShowIfNone(string alternativeText = "None")`** - Returns alternative text if string is null or empty
  ```csharp
  string? emptyString = null;
  emptyString.ShowIfNone() // Returns "None"
  emptyString.ShowIfNone("N/A") // Returns "N/A"
  "Hello".ShowIfNone() // Returns "Hello"
  ```
- **`ToSentenceCase()`** - Converts camelCase or PascalCase to sentence case
  ```csharp
  "thisIsATest".ToSentenceCase() // Returns "this is a test"
  "PascalCaseExample".ToSentenceCase() // Returns "pascal case example"
  ```
- **`SplitCamelCase()`** - Splits camel case text into separate words
  ```csharp
  "camelCaseString".SplitCamelCase() // Returns "camel Case String"
  "HTTPSConnection".SplitCamelCase() // Returns "HTTPS Connection"
  ```
- **`Capitalize(CultureInfo? culture = null)`** - Capitalizes the first character
  ```csharp
  "hello".Capitalize() // Returns "Hello"
  ```
- **`Left(int length)`** / **`Right(int length)`** - Gets leftmost or rightmost characters
  ```csharp
  "Hello World".Left(5) // Returns "Hello"
  "Hello World".Right(5) // Returns "World"
  ```
- **`Reverse()`** - Reverses a string
  ```csharp
  "Hello".Reverse() // Returns "olleH"
  ```
- **`ParseDateFromDateTimePicker(CultureInfo? culture = null)`** - Parses yyyy/MM/dd format dates
  ```csharp
  "2024/12/25".ParseDateFromDateTimePicker() // Returns DateTime(2024, 12, 25)
  "invalid".ParseDateFromDateTimePicker() // Returns null
  ```

### DateTime Extensions & Validation
- **`MustComeBefore` Attribute** - Validation attribute to ensure one DateTime property precedes another
  ```csharp
  public class TimePeriod
  {
      [MustComeBefore("EndDate")]
      public DateTime StartDate { get; set; }
      public DateTime EndDate { get; set; }
  }
  ```

### Function Memoization
- **`Memoize()`** - Automatically caches function results for improved performance
  ```csharp
  Func<int, int> expensiveOperation = x => { /* ... */ };
  var memoized = expensiveOperation.Memoize();
  // First call computes and caches result
  var result1 = memoized(5);
  // Subsequent calls with same input return cached result
  var result2 = memoized(5); // Instant return from cache
  ```
  - Supports functions with up to two arguments
  - Note: Not optimized for recursive functions

### ExtendedDictionary
- Thread-safe dictionary implementation with improved performance characteristics

## Installation

Add the package reference to your project:

```xml
<PackageReference Include="Eventualist.Extensions" Version="5.0.0.1" />
```

Or via the .NET CLI:

```bash
dotnet add package Eventualist.Extensions
```

## Compatibility

- **Target Framework**: .NET 10.0 (net10.0)
- **C# Language Version**: 14.0
- **License**: MIT

## Version History

- **5.0.0.1**: Major upgrade to .NET 10.0 and C# 14
  - Refactored all extension methods to use modern `extension` syntax for improved readability
  - Enhanced Boolean, Collection, DateTime, Function, and String extensions with better null handling
  - Improved XML documentation across all methods
  - Consolidated nullable and non-nullable overloads where applicable
  - Updated dependencies to latest versions
  - Added support for multiple `MustComeBefore` attributes on a single property
  - Optimized string truncation logic for better clarity
  - Code cleanup and modernization following C# 14 conventions
  - Improved testability and backward compatibility
  - Streamlined CI/CD workflow with strong-name key provisioning
- **4.0.0.3-dev0004**: Updated to .NET 10 and C# 14, refreshed CI workflow
- **3.x**: Updates for .NET 9 compatibility
- **2.0.0.0**: .NET 6.0 compatibility (use version 1.0.0.19 for older frameworks)
- **1.0.0.13**: Added Memoize functionality
- **1.0.0.0**: Initial release with Bool and Collection extensions

## Building and Testing

The project includes a GitHub Actions CI workflow (`.github/workflows/dotnet.yml`) that automatically:
- Restores NuGet packages
- Builds the solution using .NET 10 SDK
- Runs all unit tests

### Local Development

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Create NuGet package
dotnet pack
```

## Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch
3. Run `dotnet test` locally to ensure all tests pass
4. Submit a pull request

## Author

**Iede Snoek**  
Email: info@esoxsolutions.nl  
Company: Esox Solutions

## License

This project is licensed under the MIT License - see the LICENSE file for details.

Copyright (c) 2022-2025 Esox Solutions