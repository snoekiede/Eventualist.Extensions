# Security Policy

## Supported Versions

The following versions of Eventualist.Extensions are currently supported with security updates:

| Version | Supported          | .NET Target |
| ------- | ------------------ | ----------- |
| 5.0.x   | :white_check_mark: | .NET 10.0   |
| 4.0.x   | :x:                | .NET 10.0   |
| 3.x     | :x:                | .NET 9.0    |
| 2.x     | :x:                | .NET 6.0    |
| < 2.0   | :x:                | Legacy      |

**Note**: Only the latest major version (5.x) receives security updates. Users are strongly encouraged to upgrade to the latest version.

## Security Considerations

### Library Scope

Eventualist.Extensions is a collection of lightweight utility extension methods for common .NET types. The library:

- Does not handle sensitive data (passwords, tokens, keys, etc.)
- Does not perform network operations
- Does not directly interact with file systems (except for MIME type detection)
- Does not execute external commands or processes
- Does not use reflection for security-sensitive operations

### Potential Security Concerns

While the library is designed to be safe, users should be aware of the following:

1. **HTML Stripping**: The `StripHtml()` method removes HTML tags but is **not designed for security sanitization**. It should not be relied upon to prevent XSS attacks or sanitize user input for security purposes. Use dedicated security libraries for input sanitization.

2. **String Manipulation**: Methods like `Truncate()`, `Abbreviate()`, and `Left()`/`Right()` do not validate or sanitize input for security purposes. They are purely formatting utilities.

3. **Memoization**: The `Memoize()` extension caches function results in memory. Be cautious when memoizing functions that process sensitive data, as cached values persist in memory.

4. **File Extensions**: The `HasCorrectExtension()` method validates file extensions but should **not be used as the sole security measure** for file upload validation. Always combine with proper file type verification, content inspection, and server-side validation.

## Reporting a Vulnerability

We take security seriously. If you discover a security vulnerability in Eventualist.Extensions, please follow these steps:

### How to Report

**Please do NOT report security vulnerabilities through public GitHub issues.**

Instead, please report security vulnerabilities by email to:

?? **info@esoxsolutions.nl**

### What to Include

Please include the following information in your report:

- **Description**: A clear description of the vulnerability
- **Impact**: The potential impact and severity of the issue
- **Reproduction**: Step-by-step instructions to reproduce the vulnerability
- **Version**: The affected version(s) of Eventualist.Extensions
- **Environment**: .NET version, OS, and any other relevant environment details
- **Proof of Concept**: Code samples or test cases demonstrating the issue (if available)
- **Suggested Fix**: If you have a suggested fix, please include it

### What to Expect

- **Acknowledgment**: You will receive an acknowledgment of your report within **48 hours**
- **Initial Assessment**: We will provide an initial assessment within **5 business days**
- **Updates**: We will keep you informed of our progress throughout the investigation
- **Resolution**: We aim to release a security patch within **30 days** for confirmed vulnerabilities
- **Credit**: With your permission, we will publicly acknowledge your responsible disclosure

### Security Update Process

1. **Investigation**: We will investigate and verify the reported vulnerability
2. **Fix Development**: We will develop and test a fix
3. **Security Advisory**: We will create a security advisory on GitHub
4. **Patch Release**: We will release a patched version
5. **Notification**: We will notify users through:
   - GitHub Security Advisory
   - Release notes
   - NuGet package update

## Security Best Practices for Users

When using Eventualist.Extensions in your projects:

1. **Keep Updated**: Always use the latest version to benefit from security patches and improvements

2. **Input Validation**: Never rely solely on these extension methods for security-critical input validation or sanitization

3. **HTML Content**: Do not use `StripHtml()` for XSS prevention. Use established security libraries like:
   - [HtmlSanitizer](https://www.nuget.org/packages/HtmlSanitizer/)
   - [Microsoft.Security.Application.AntiXss](https://www.nuget.org/packages/AntiXSS/)

4. **File Uploads**: When validating file uploads:
   - Combine `HasCorrectExtension()` with proper MIME type checking
   - Scan uploaded files with antivirus software
   - Validate file content, not just extension
   - Limit file sizes
   - Store uploads outside the web root

5. **Sensitive Data**: Be cautious when using `Memoize()` with functions that process sensitive data. Consider:
   - The lifetime of cached data
   - Memory exposure risks
   - Implementing custom caching with proper security controls

6. **Dependency Management**: Regularly audit your dependencies:
   ```bash
   dotnet list package --vulnerable
   dotnet list package --outdated
   ```

## Known Non-Issues

The following are **not** considered security vulnerabilities:

- Missing input validation in utility methods (responsibility lies with the caller)
- Performance characteristics of specific methods
- Lack of built-in rate limiting or throttling
- Memory usage patterns of memoization
- Behavior differences across different .NET versions

## Scope

This security policy applies to:
- The Eventualist.Extensions library code
- Official NuGet packages published under the "Eventualist.Extensions" name
- Official releases and pre-releases

This security policy does **not** apply to:
- Third-party forks or modifications
- Sample code or examples in documentation
- Development or test code not included in the published package

## Disclaimer

?? **Important**: This library is provided "as is" without warranty of any kind. While we make every effort to address security concerns promptly, this is a personal project maintained on a best-effort basis. For production use in security-sensitive contexts, please:

- Thoroughly test the library in your specific environment
- Conduct your own security review
- Consider the risks and limitations
- Pin to specific versions rather than using wildcards

## Security Contact

For security-related questions or concerns:

?? Email: info@esoxsolutions.nl  
?? Company: Esox Solutions  
?? GitHub: [@snoekiede](https://github.com/snoekiede)

## Additional Resources

- [OWASP Top Ten](https://owasp.org/www-project-top-ten/)
- [.NET Security Best Practices](https://docs.microsoft.com/en-us/dotnet/standard/security/)
- [GitHub Security Advisories](https://github.com/snoekiede/Eventualist.Extensions/security/advisories)

---

Last Updated: December 2025  
Version: 1.0
