using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
