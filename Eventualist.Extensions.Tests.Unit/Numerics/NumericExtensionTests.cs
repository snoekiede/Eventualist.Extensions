using System;
using Eventualist.Extensions.Numerics;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Numerics
{
    public class NumericExtensionTests
    {
        #region IsEven Tests

        [Theory]
        [InlineData(0, true)]
        [InlineData(2, true)]
        [InlineData(4, true)]
        [InlineData(-2, true)]
        [InlineData(1, false)]
        [InlineData(3, false)]
        [InlineData(-1, false)]
        public void IsEven_WithIntegers_ReturnsCorrectResult(int number, bool expected)
        {
            Assert.Equal(expected, number.IsEven());
        }

        [Theory]
        [InlineData(0L, true)]
        [InlineData(2L, true)]
        [InlineData(1L, false)]
        public void IsEven_WithLongs_ReturnsCorrectResult(long number, bool expected)
        {
            Assert.Equal(expected, number.IsEven());
        }

        #endregion

        #region IsOdd Tests

        [Theory]
        [InlineData(1, true)]
        [InlineData(3, true)]
        [InlineData(-1, true)]
        [InlineData(0, false)]
        [InlineData(2, false)]
        [InlineData(-2, false)]
        public void IsOdd_WithIntegers_ReturnsCorrectResult(int number, bool expected)
        {
            Assert.Equal(expected, number.IsOdd());
        }

        [Theory]
        [InlineData(1L, true)]
        [InlineData(2L, false)]
        public void IsOdd_WithLongs_ReturnsCorrectResult(long number, bool expected)
        {
            Assert.Equal(expected, number.IsOdd());
        }

        #endregion

        #region IsBetween Tests

        [Theory]
        [InlineData(5, 1, 10, true)]
        [InlineData(1, 1, 10, true)]
        [InlineData(10, 1, 10, true)]
        [InlineData(0, 1, 10, false)]
        [InlineData(11, 1, 10, false)]
        [InlineData(-5, -10, 0, true)]
        public void IsBetween_WithIntegers_ReturnsCorrectResult(int number, int min, int max, bool expected)
        {
            Assert.Equal(expected, number.IsBetween(min, max));
        }

        [Theory]
        [InlineData(5.5, 1.0, 10.0, true)]
        [InlineData(0.5, 1.0, 10.0, false)]
        [InlineData(10.5, 1.0, 10.0, false)]
        public void IsBetween_WithDoubles_ReturnsCorrectResult(double number, double min, double max, bool expected)
        {
            Assert.Equal(expected, number.IsBetween(min, max));
        }

        [Fact]
        public void IsBetween_WithDecimals_ReturnsCorrectResult()
        {
            Assert.True(5.5m.IsBetween(1.0m, 10.0m));
            Assert.False(0.5m.IsBetween(1.0m, 10.0m));
        }

        #endregion

        #region Clamp Tests

        [Theory]
        [InlineData(5, 0, 10, 5)]
        [InlineData(15, 0, 10, 10)]
        [InlineData(-5, 0, 10, 0)]
        [InlineData(10, 0, 10, 10)]
        [InlineData(0, 0, 10, 0)]
        public void Clamp_WithIntegers_ClampsCorrectly(int number, int min, int max, int expected)
        {
            Assert.Equal(expected, number.Clamp(min, max));
        }

        [Theory]
        [InlineData(5.5, 0.0, 10.0, 5.5)]
        [InlineData(15.5, 0.0, 10.0, 10.0)]
        [InlineData(-5.5, 0.0, 10.0, 0.0)]
        public void Clamp_WithDoubles_ClampsCorrectly(double number, double min, double max, double expected)
        {
            Assert.Equal(expected, number.Clamp(min, max));
        }

        [Fact]
        public void Clamp_WithDecimals_ClampsCorrectly()
        {
            Assert.Equal(5.5m, 5.5m.Clamp(0.0m, 10.0m));
            Assert.Equal(10.0m, 15.5m.Clamp(0.0m, 10.0m));
        }

        [Fact]
        public void Clamp_WithMinGreaterThanMax_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => 5.Clamp(10, 0));
            Assert.Throws<ArgumentException>(() => 5.0.Clamp(10.0, 0.0));
            Assert.Throws<ArgumentException>(() => 5.0m.Clamp(10.0m, 0.0m));
        }

        #endregion

        #region ToOrdinal Tests

        [Theory]
        [InlineData(1, "1st")]
        [InlineData(2, "2nd")]
        [InlineData(3, "3rd")]
        [InlineData(4, "4th")]
        [InlineData(11, "11th")]
        [InlineData(12, "12th")]
        [InlineData(13, "13th")]
        [InlineData(21, "21st")]
        [InlineData(22, "22nd")]
        [InlineData(23, "23rd")]
        [InlineData(24, "24th")]
        [InlineData(101, "101st")]
        [InlineData(112, "112th")]
        [InlineData(113, "113th")]
        [InlineData(121, "121st")]
        public void ToOrdinal_WithPositiveNumbers_ReturnsCorrectOrdinal(int number, string expected)
        {
            Assert.Equal(expected, number.ToOrdinal());
        }

        [Theory]
        [InlineData(0, "0")]
        [InlineData(-1, "-1")]
        public void ToOrdinal_WithNonPositiveNumbers_ReturnsNumberAsString(int number, string expected)
        {
            Assert.Equal(expected, number.ToOrdinal());
        }

        #endregion
    }
}
