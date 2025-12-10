# Contributing to Eventualist.Extensions

Thank you for your interest in contributing to Eventualist.Extensions! This document provides guidelines and information for contributors.

## ?? Important Notice

This library is primarily developed for personal use and experimentation. While contributions are welcome, please note:

- Changes may be accepted or declined based on the project maintainer's needs and vision
- Response times may vary as this is maintained on a best-effort basis
- Breaking changes may occur between versions without extensive deprecation notices

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- A code editor (Visual Studio 2025+, VS Code, or Rider recommended)
- Git for version control

### Setting Up Your Development Environment

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/YOUR-USERNAME/Eventualist.Extensions.git
   cd Eventualist.Extensions
   ```

3. **Add the upstream repository**:
   ```bash
   git remote add upstream https://github.com/snoekiede/Eventualist.Extensions.git
   ```

4. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

5. **Build the solution**:
   ```bash
   dotnet build
   ```

6. **Run the tests**:
   ```bash
   dotnet test
   ```

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue on GitHub with:

- A clear, descriptive title
- Detailed steps to reproduce the issue
- Expected behavior vs. actual behavior
- Your environment details (.NET version, OS, etc.)
- Code samples or test cases demonstrating the issue (if applicable)

### Suggesting Enhancements

For feature requests or enhancements:

- Check if a similar suggestion already exists in the issues
- Provide a clear use case explaining why the feature would be valuable
- Include example code showing how the feature would be used
- Keep in mind this is a focused library of lightweight extensions

### Pull Requests

1. **Create a feature branch** from `main`:
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes**:
   - Follow the existing code style and conventions
   - Write or update tests for your changes
   - Update documentation as needed
   - Keep changes focused and atomic

3. **Test your changes** thoroughly:
   ```bash
   dotnet test
   ```

4. **Commit your changes**:
   ```bash
   git add .
   git commit -m "Brief description of your changes"
   ```
   - Use clear, descriptive commit messages
   - Reference issue numbers when applicable (e.g., "Fix #123: Description")

5. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Create a Pull Request**:
   - Provide a clear description of the changes
   - Reference any related issues
   - Ensure all CI checks pass

## Coding Standards

### General Guidelines

- **Target Framework**: .NET 10.0
- **Language Version**: C# 14
- **Code Style**: Follow existing conventions in the codebase
- **Nullable Reference Types**: Use nullable annotations (`Nullable: annotations`)

### Extension Method Guidelines

- Use the modern `extension` syntax introduced in C# 14 where appropriate
- Provide comprehensive XML documentation with:
  - Summary description
  - Parameter descriptions
  - Return value description
  - Example usage (when helpful)
- Handle null cases appropriately
- Consider performance implications
- Keep methods focused and single-purpose

### Example:

```csharp
/// <summary>
/// Converts a string to title case by capitalizing the first letter.
/// </summary>
/// <param name="value">The string to convert.</param>
/// <returns>The string in title case.</returns>
/// <example>
/// <code>
/// "hello world".Titleize() // Returns "Hello World"
/// </code>
/// </example>
public static string Titleize(this string value)
{
    // Implementation
}
```

### Testing Standards

- Write unit tests for all new functionality
- Use xUnit testing framework
- Follow the Arrange-Act-Assert pattern
- Test edge cases and null handling
- Aim for high code coverage
- Name tests descriptively: `MethodName_Scenario_ExpectedResult`

### Example Test:

```csharp
[Fact]
public void Titleize_WithLowercaseString_ReturnsCapitalized()
{
    // Arrange
    var input = "hello world";
    
    // Act
    var result = input.Titleize();
    
    // Assert
    Assert.Equal("Hello World", result);
}
```

## Documentation

- Update the README.md if you add new features
- Add XML documentation comments to all public methods
- Include code examples for complex functionality
- Keep documentation clear and concise

## Commit Message Guidelines

Follow this format for commit messages:

```
Brief summary (50 chars or less)

More detailed explanation if needed. Wrap at 72 characters.
Explain the problem that this commit is solving, and why the
change is needed.

- Use bullet points for multiple items
- Reference issues with #issue-number
```

## Code Review Process

- All submissions require review before merging
- Maintainer may request changes or provide feedback
- Be patient and respectful during the review process
- Address review comments promptly

## Testing

Before submitting a pull request:

1. **Run all tests**:
   ```bash
   dotnet test
   ```

2. **Verify the build succeeds**:
   ```bash
   dotnet build --configuration Release
   ```

3. **Check for compilation warnings**:
   - Aim for zero warnings in your changes

## CI/CD

The project uses GitHub Actions for continuous integration:
- Automatic build on all pull requests
- Test execution on Windows (windows-latest)
- Strong-name signing for releases

Your pull request must pass all CI checks before it can be merged.

## License

By contributing to Eventualist.Extensions, you agree that your contributions will be licensed under the MIT License, the same license that covers the project.

## Questions?

If you have questions or need help:
- Check existing issues and discussions
- Create a new issue with the "question" label
- Contact: info@esoxsolutions.nl

## Recognition

Contributors will be acknowledged in release notes and appreciated for their efforts!

---

Thank you for contributing to Eventualist.Extensions! ??
