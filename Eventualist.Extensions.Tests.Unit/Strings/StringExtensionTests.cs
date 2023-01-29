using Eventualist.Extensions.Strings;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Strings
{
    public class StringExtensionTests
    {
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
        public void HasCorrectExtensionTestForCorrectExtensionForPng()
        {
            var resultForPng = "picture.png".HasCorrectExtension();
            Assert.True(resultForPng);
        }

        [Fact]
        public void HasCorrectExtensionTestForCorrectExtensionForJpg()
        {
            var resultForPng = "picture.jpg".HasCorrectExtension();
            Assert.True(resultForPng);
        }

        [Fact]
        public void HasCorrectExtensionTestForCorrectExtensionForJpeg()
        {
            var resultForPng = "picture.jpeg".HasCorrectExtension();
            Assert.True(resultForPng);
        }

        [Fact]
        public void HasCorrectExtensionTestForCorrectExtensionForGif()
        {
            var resultForPng = "picture.gif".HasCorrectExtension();
            Assert.True(resultForPng);
        }

        [Fact]
        public void HasCorrectExtensionTestForCorrectExtensionForIncorrectExtension()
        {
            var resultForPng = "picture.mp3".HasCorrectExtension();
            Assert.False(resultForPng);
        }

        [Fact]
        public void TestConvertToMimeTypePng()
        {
            var result = ".png".ConvertToMimeType();
            Assert.Equal("image/png",result);
        }

        [Fact]
        public void TestConvertToMimeTypeJpg()
        {
            var result = ".jpg".ConvertToMimeType();
            Assert.Equal("image/jpeg", result);
        }

        [Fact]
        public void TestConvertToMimeTypeJpeg()
        {
            var result = ".jpeg".ConvertToMimeType();
            Assert.Equal("image/jpeg", result);
        }

        [Fact]
        public void TestConvertToMimeTypeGif()
        {
            var result = ".gif".ConvertToMimeType();
            Assert.Equal("image/gif", result);
        }

        [Fact]
        public void ParseCorrectDateFromDateTimePicker()
        {
            var result = "19/07/1968".ParseDateFromDateTimePicker();
            Assert.NotNull(result);
            Assert.Equal(19,result.Value.Day);
            Assert.Equal(7,result.Value.Month);
            Assert.Equal(1968,result.Value.Year);
        }

        [Fact]
        public void ParseIncorrectDateFails()
        {
            var result = "19/07".ParseDateFromDateTimePicker();
            Assert.Null(result);
        }
    }
}
