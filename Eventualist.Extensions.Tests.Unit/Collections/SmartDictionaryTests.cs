using System;
using Eventualist.Extensions.Collections;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Collections
{
    public class SmartDictionaryTests
    {
        [Fact]
        public void TestInitialize()
        {
            var dictionary = new ExtendedDictionary<string, string>();
            Assert.NotNull(dictionary);
        }

        [Fact]
        public void NormalGetAndSetShouldWork()
        {
            var dictionary = new ExtendedDictionary<string, string>
            {
                ["me"] = "developer"
            };
            var retrievedKey = dictionary["me"];
            Assert.Equal("developer",retrievedKey);
        }

        [Fact]
        public void NonExistentKeyShouldReturnGivenDefaultKey()
        {
            var dictionary = new ExtendedDictionary<string, int>();
            var retrievedKey = dictionary["me", 0];
            Assert.Equal(0, retrievedKey);
        }

        [Fact]
        public void NonExistentKeyShouldReturnDefault()
        {
            var dictionary = new ExtendedDictionary<string, int>();
            var retrievedKey = dictionary["me"];
            Assert.Equal(0,retrievedKey);
        }

        [Fact]
        public void CanNotSetNullKey()
        {
            var dictionary=new ExtendedDictionary<string, int>();
            Assert.Throws<ArgumentNullException>(() => dictionary[null] = 0);
        }

        [Fact]
        public void CanNotGetNullKey()
        {
            var dictionary = new ExtendedDictionary<string, int>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                var temp = dictionary[null];
            });
        }

        [Fact]
        public void CanNotGetNullKeyWithDefaultValue()
        {
            var dictionary = new ExtendedDictionary<string, int>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                var temp = dictionary[null, 0];
            });
        }
    }
}
