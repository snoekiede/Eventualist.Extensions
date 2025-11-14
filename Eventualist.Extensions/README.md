# Eventualist.Extensions

Small, focused extension methods for common .NET types.

Features
- Bool helpers: `AddNot`, `ToYesOrNo`.
- Collection helpers: `IsEmpty`, `IsNotEmpty`, `Divide` (split a collection into sublists of a given maximum length).
- String helpers: `Titleize`, `Abbreviate`, `Truncate`, `StripHtml`, `HasCorrectExtension`, `ConvertToMimeType`.
- Date helpers and validation: `MustComeBefore` attribute to ensure one `DateTime` property precedes another.
- Function memoization: `Memoize` with multi-argument support.
- `ExtendedDictionary`: improved performance and thread-safety.

Compatibility
- Target framework: .NET 10 (net10.0)

Recent changes
- 4.0.0.3-dev0004: Updated to .NET 10 and refreshed CI workflow.
- Previous: updates for .NET 9/8/7 and enhancements to memoization, string utilities, and validation attributes.

Usage example
```csharp
internal class TimePeriod
{
    [MustComeBefore("EndDate")]
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
```

Building and CI
- A GitHub Actions workflow is included at `.github/workflows/dotnet.yml` which restores, builds and tests using the .NET 10 SDK.

Contributing
- Pull requests are welcome. Please run `dotnet test` locally and ensure all tests pass.

License
- MIT