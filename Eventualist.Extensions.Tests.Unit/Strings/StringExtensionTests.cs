    using System;
using System.Globalization;
using Eventualist.Extensions.Strings;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Strings
{
    public class StringExtensionTests
    {
        #region ShowIfNone Tests

        [Fact]
        public void TestShowIfNoneNotEmptyString()
        {
            var testString = "Wie, wie?";
            var processed = testString.ShowIfNone();
            Assert.Equal(testString, processed);
        }

        [Fact]
        public void TestShowIfNoneEmptyString()
        {
            var testString = "";
            var processed = testString.ShowIfNone();
            Assert.Equal("None", processed);
        }

        [Fact]
        public void TestShowIfNoneNotEmptyStringOtherNone()
        {
            var testString = "Wie, wie?";
            var processed = testString.ShowIfNone("Niets");
            Assert.Equal(testString, processed);
        }

        [Fact]
        public void TestShowIfNoneEmptyStringOtherNone()
        {
            var testString = "";
            var processed = testString.ShowIfNone("Niets");
            Assert.Equal("Niets", processed);
        }

        [Fact]
        public void TestShowIfNoneWithNullString()
        {
            string? testString = null;
            var processed = ((string)null).ShowIfNone("Default");
            Assert.Equal("Default", processed);
        }

        #endregion

        
        #region HasCorrectExtension Tests

        [Theory]
        [InlineData("picture.png", true)]
        [InlineData("picture.jpg", true)]
        [InlineData("picture.jpeg", true)]
        [InlineData("picture.gif", true)]
        [InlineData("picture.bmp", true)]
        [InlineData("picture.tiff", true)]
        [InlineData("picture.webp", true)]
        [InlineData("picture.mp3", false)]
        [InlineData("picture.doc", false)]
        [InlineData("picture", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void TestHasImageExtension(string? filename, bool expected)
        {
            Assert.Equal(expected, filename.HasCorrectExtension());
        }

        [Fact]
        public void TestHasImageExtension_WithAdditionalExtensions_IncludesCustomExtensions()
        {
            Assert.True("image.svg".HasCorrectExtension("svg"));
            Assert.True("image.custom".HasCorrectExtension("custom", "other"));
        }

        [Fact]
        public void TestHasImageExtension_WithCaseInsensitivity_WorksCorrectly()
        {
            Assert.True("image.PNG".HasCorrectExtension());
            Assert.True("image.Jpg".HasCorrectExtension());
        }

        #endregion

        #region ConvertToMimeType Tests

        [Theory]
        [InlineData(".png", "image/png")]
        [InlineData(".jpg", "image/jpeg")]
        [InlineData(".jpeg", "image/jpeg")]
        [InlineData(".gif", "image/gif")]
        [InlineData(".pdf", "application/pdf")]
        [InlineData(".doc", "application/msword")]
        [InlineData(".unknown", "application/octet-stream")]
        [InlineData("", "application/octet-stream")]
        public void TestGetMimeType(string extension, string expected)
        {
            Assert.Equal(expected, extension.ConvertToMimeType());
        }

        [Fact]
        public void TestGetMimeType_WithoutLeadingDot_WorksCorrectly()
        {
            Assert.Equal("image/png", "png".ConvertToMimeType());
            Assert.Equal("image/jpeg", "jpg".ConvertToMimeType());
        }

        [Fact]
        public void TestGetMimeType_WithMixedCase_WorksCorrectly()
        {
            Assert.Equal("image/png", "PNG".ConvertToMimeType());
            Assert.Equal("image/jpeg", "Jpg".ConvertToMimeType());
        }

        #endregion

        #region ParseDateFromDateTimePicker Tests

        [Fact]
        public void ParseCorrectDateFromDateTimePicker()
        {
            var result = "1968/07/19".ParseDateFromDateTimePicker();
            Assert.NotNull(result);
            Assert.Equal(19, result.Value.Day);
            Assert.Equal(7, result.Value.Month);
            Assert.Equal(1968, result.Value.Year);
        }

        [Theory]
        [InlineData("19/07")]
        [InlineData("not-a-date")]
        [InlineData("")]
        [InlineData(null)]
        public void ParseInvalidDate_ReturnsNull(string? dateString)
        {
            var result = dateString.ParseDateFromDateTimePicker();
            Assert.Null(result);
        }

        [Fact]
        public void ParseDateWithCulture_WorksCorrectly()
        {
            var frenchCulture = new CultureInfo("fr-FR");
            var result = "1968/07/19".ParseDateFromDateTimePicker(frenchCulture);
            Assert.NotNull(result);
            Assert.Equal(new DateTime(1968, 7, 19), result.Value);
        }

        #endregion

        #region Titleize Tests

        [Theory]
        [InlineData("hello world", "Hello World")]
        [InlineData("HELLO WORLD", "Hello World")]
        [InlineData("hello_world", "Hello_world")]
        [InlineData("helloWorld", "Hello World")]
        [InlineData("HelloWorld", "Hello World")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void TestTitleize(string? input, string expected)
        {
            Assert.Equal(expected, input.Titleize());
        }

        [Fact]
        public void TestTitleize_WithCulture_AppliesCultureSpecificRules()
        {
            var turkishCulture = new CultureInfo("tr-TR");
            // In Turkish, lowercase 'i' converts to uppercase 'İ' with a dot
            Assert.Equal("İnformation", "information".Titleize(turkishCulture));
        }

        #endregion

        #region SplitCamelCase Tests

        [Theory]
        [InlineData("HelloWorld", "Hello World")]
        [InlineData("helloWorld", "hello World")]
        [InlineData("ABCTest", "ABC Test")]
        [InlineData("HTML5Test", "HTML5 Test")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void TestSplitCamelCase(string? input, string expected)
        {
            Assert.Equal(expected, input.SplitCamelCase());
        }

        #endregion

        #region Abbreviate Tests

        [Theory]
        [InlineData("This is a short string", 30, "This is a short string")]
        [InlineData("This is a very long string that should be abbreviated", 20, "This is a very...")]
        [InlineData("NoSpaces", 3, "...")]
        [InlineData("", 10, "")]
        [InlineData(null, 10, "")]
        public void TestAbbreviate(string? input, int maxLength, string expected)
        {
            Assert.Equal(expected, input.Abbreviate(maxLength));
        }

        [Fact]
        public void TestAbbreviate_WithCustomSymbol_UsesCustomSymbol()
        {
            var result = "This is a long text".Abbreviate(10, " [more]");
            Assert.Equal("This is [more]", result);
        }

        [Fact]
        public void TestAbbreviate_WithNegativeLength_ReturnsEmptyString()
        {
            Assert.Equal("", "Hello World".Abbreviate(-5));
        }

        #endregion

        #region Truncate Tests

        [Theory]
        [InlineData("Hello World", 5, "He...")]
        [InlineData("Hello", 10, "Hello")]
        [InlineData("", 5, "")]
        [InlineData(null, 5, "")]
        public void TestTruncate(string? input, int maxLength, string expected)
        {
            Assert.Equal(expected, input.Truncate(maxLength));
        }

        [Fact]
        public void TestTruncate_WithCustomSuffix_UsesCustomSuffix()
        {
            Assert.Equal("Hel[more]", "Hello World".Truncate(8, "[more]"));
        }

        [Fact]
        public void TestTruncate_WithNegativeLength_ReturnsEmptyString()
        {
            Assert.Equal("", "Hello World".Truncate(-5));
        }

        #endregion

        #region StripHtml Tests

        [Theory]
        [InlineData("<p>Hello World</p>", "Hello World")]
        [InlineData("<strong>Bold</strong> and <em>italic</em>", "Bold and italic")]
        [InlineData("<div><p>Nested <span>tags</span></p></div>", "Nested tags")]
        [InlineData("No HTML", "No HTML")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void TestStripHtml(string? input, string expected)
        {
            Assert.Equal(expected, input.StripHtml());
        }

        #endregion

        #region Left Tests

        [Theory]
        [InlineData("Hello World", 5, "Hello")]
        [InlineData("Hello", 10, "Hello")]
        [InlineData("", 5, "")]
        [InlineData(null, 5, "")]
        public void TestLeft(string? input, int length, string expected)
        {
            Assert.Equal(expected, input.Left(length));
        }

        [Fact]
        public void TestLeft_WithNegativeLength_ReturnsEmptyString()
        {
            Assert.Equal("", "Hello World".Left(-5));
        }

        #endregion

        #region Right Tests

        [Theory]
        [InlineData("Hello World", 5, "World")]
        [InlineData("Hello", 10, "Hello")]
        [InlineData("", 5, "")]
        [InlineData(null, 5, "")]
        public void TestRight(string? input, int length, string expected)
        {
            Assert.Equal(expected, input.Right(length));
        }

        [Fact]
        public void TestRight_WithNegativeLength_ReturnsEmptyString()
        {
            Assert.Equal("", "Hello World".Right(-5));
        }

        #endregion

        #region Capitalize Tests

        [Theory]
        [InlineData("hello", "Hello")]
        [InlineData("Hello", "Hello")]
        [InlineData("h", "H")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void TestCapitalize(string? input, string expected)
        {
            Assert.Equal(expected, input.Capitalize());
        }

        [Fact]
        public void TestCapitalize_WithCulture_AppliesCultureSpecificRules()
        {
            var turkishCulture = new CultureInfo("tr-TR");
            // In Turkish, lowercase 'i' converts to uppercase 'İ' with a dot
            Assert.Equal("İnformation", "information".Capitalize(turkishCulture));
        }

        #endregion

        #region Reverse Tests

        [Theory]
        [InlineData("Hello", "olleH")]
        [InlineData("12345", "54321")]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void TestReverse(string? input, string expected)
        {
            Assert.Equal(expected, input.Reverse());
        }

        #endregion
    }
}
