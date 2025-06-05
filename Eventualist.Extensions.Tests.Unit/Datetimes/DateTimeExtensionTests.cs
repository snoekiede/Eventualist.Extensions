using System;
using System.Globalization;
using Eventualist.Extensions.Datetimes;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Datetimes
{
    public class DateTimeExtensionTests
    {
        private readonly DateTime _testDate = new(2019, 12, 31, 15, 30, 45);
        private readonly DateTime? _nullableTestDate = new DateTime(2019, 12, 31, 15, 30, 45);
        private readonly DateTime? _nullDate = null;

        #region Format Tests

        [Fact]
        public void TestFormatDateForPicker()
        {
            var formatted = _testDate.FormatDateForPicker();
            Assert.Equal("2019/12/31", formatted);
        }

        [Fact]
        public void TestFormatTimeForPicker()
        {
            var formatted = _testDate.FormatTimeForPicker();
            Assert.Equal("15:30", formatted);
        }

        [Fact]
        public void TestFormatDateTimeForPicker()
        {
            var formatted = _testDate.FormatDateTimeForPicker();
            Assert.Equal("2019/12/31 15:30", formatted);
        }

        [Fact]
        public void TestFormatForPicker_WithCustomFormat()
        {
            var formatted = _testDate.FormatForPicker("MM/dd/yyyy HH:mm:ss");
            Assert.Equal("12/31/2019 15:30:45", formatted);
        }

        [Fact]
        public void TestFormatWithSpecificCulture()
        {
            var frenchCulture = new CultureInfo("fr-FR");
            var germanCulture = new CultureInfo("de-DE");
            
            // Even with different cultures, the format should be consistent for pickers
            Assert.Equal("2019/12/31", _testDate.FormatDateForPicker(frenchCulture));
            Assert.Equal("15:30", _testDate.FormatTimeForPicker(germanCulture));
        }

        #endregion

        #region Nullable DateTime Tests

        [Fact]
        public void TestNullableFormatDateForPicker_WithValue()
        {
            var formatted = _nullableTestDate.FormatDateForPicker();
            Assert.Equal("2019/12/31", formatted);
        }

        [Fact]
        public void TestNullableFormatDateForPicker_WithNull()
        {
            var formatted = _nullDate.FormatDateForPicker();
            Assert.Equal("", formatted);
        }

        [Fact]
        public void TestNullableFormatDateForPicker_WithCustomDefault()
        {
            var formatted = _nullDate.FormatDateForPicker("N/A");
            Assert.Equal("N/A", formatted);
        }

        [Fact]
        public void TestNullableFormatTimeForPicker_WithValue()
        {
            var formatted = _nullableTestDate.FormatTimeForPicker();
            Assert.Equal("15:30", formatted);
        }

        [Fact]
        public void TestNullableFormatTimeForPicker_WithNull()
        {
            var formatted = _nullDate.FormatTimeForPicker();
            Assert.Equal("", formatted);
        }

        [Fact]
        public void TestNullableFormatDateTimeForPicker_WithValue()
        {
            var formatted = _nullableTestDate.FormatDateTimeForPicker();
            Assert.Equal("2019/12/31 15:30", formatted);
        }

        [Fact]
        public void TestNullableFormatDateTimeForPicker_WithNull()
        {
            var formatted = _nullDate.FormatDateTimeForPicker();
            Assert.Equal("", formatted);
        }

        #endregion

        #region Date Utility Tests

        [Fact]
        public void TestStartOfDay()
        {
            var result = _testDate.StartOfDay();
            
            Assert.Equal(2019, result.Year);
            Assert.Equal(12, result.Month);
            Assert.Equal(31, result.Day);
            Assert.Equal(0, result.Hour);
            Assert.Equal(0, result.Minute);
            Assert.Equal(0, result.Second);
            Assert.Equal(0, result.Millisecond);
        }

        [Fact]
        public void TestEndOfDay()
        {
            var result = _testDate.EndOfDay();
            
            Assert.Equal(2019, result.Year);
            Assert.Equal(12, result.Month);
            Assert.Equal(31, result.Day);
            Assert.Equal(23, result.Hour);
            Assert.Equal(59, result.Minute);
            Assert.Equal(59, result.Second);
            Assert.Equal(999, result.Millisecond);
        }

        [Fact]
        public void TestStartOfMonth()
        {
            var result = _testDate.StartOfMonth();
            
            Assert.Equal(2019, result.Year);
            Assert.Equal(12, result.Month);
            Assert.Equal(1, result.Day);
            Assert.Equal(0, result.Hour);
            Assert.Equal(0, result.Minute);
            Assert.Equal(0, result.Second);
        }

        [Fact]
        public void TestEndOfMonth()
        {
            var result = _testDate.EndOfMonth();
            
            Assert.Equal(2019, result.Year);
            Assert.Equal(12, result.Month);
            Assert.Equal(31, result.Day);
            Assert.Equal(23, result.Hour);
            Assert.Equal(59, result.Minute);
            Assert.Equal(59, result.Second);
            Assert.Equal(999, result.Millisecond);
        }

        [Theory]
        [InlineData(2019, 12, 28, true)]  // Saturday
        [InlineData(2019, 12, 29, true)]  // Sunday
        [InlineData(2019, 12, 30, false)] // Monday
        [InlineData(2019, 12, 31, false)] // Tuesday
        public void TestIsWeekend(int year, int month, int day, bool expected)
        {
            var date = new DateTime(year, month, day);
            Assert.Equal(expected, date.IsWeekend());
        }

        [Fact]
        public void TestAddBusinessDays_PositiveDays()
        {
            // Friday
            var friday = new DateTime(2019, 12, 27);
            
            // Add 1 business day: should be Monday
            var result = friday.AddBusinessDays(1);
            Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
            Assert.Equal(new DateTime(2019, 12, 30), result.Date);
            
            // Add 3 business days: should be Wednesday
            result = friday.AddBusinessDays(3);
            Assert.Equal(DayOfWeek.Wednesday, result.DayOfWeek);
            Assert.Equal(new DateTime(2020, 1, 1), result.Date);
        }

        [Fact]
        public void TestAddBusinessDays_NegativeDays()
        {
            // Wednesday
            var wednesday = new DateTime(2020, 1, 1);
            
            // Subtract 1 business day: should be Tuesday
            var result = wednesday.AddBusinessDays(-1);
            Assert.Equal(DayOfWeek.Tuesday, result.DayOfWeek);
            Assert.Equal(new DateTime(2019, 12, 31), result.Date);
            
            // Subtract 3 business days: should be Friday
            result = wednesday.AddBusinessDays(-3);
            Assert.Equal(DayOfWeek.Friday, result.DayOfWeek);
            Assert.Equal(new DateTime(2019, 12, 27), result.Date);
        }

        [Fact]
        public void TestAddBusinessDays_SkipsWeekends()
        {
            // Friday
            var friday = new DateTime(2019, 12, 27);
            
            // Add 5 business days: should be next Friday
            var result = friday.AddBusinessDays(5);
            Assert.Equal(DayOfWeek.Friday, result.DayOfWeek);
            Assert.Equal(new DateTime(2020, 1, 3), result.Date);
        }

        [Fact]
        public void TestAddBusinessDays_ZeroDays_ReturnsSameDate()
        {
            var original = new DateTime(2019, 12, 27);
            var result = original.AddBusinessDays(0);
            
            Assert.Equal(original, result);
        }

        #endregion
    }
}
