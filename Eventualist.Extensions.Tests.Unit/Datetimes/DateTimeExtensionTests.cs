using System;
using System.Collections.Generic;
using System.Text;
using Eventualist.Extensions.Datetimes;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Datetimes
{
    public class DateTimeExtensionTests
    {
        [Fact]
        public void TestFormatDateForPicker()
        {
            var testDate = new DateTime(2019, 12, 31, 15, 0, 0);
            var formatted = testDate.FormatDateForPicker();
            Assert.Equal("2019/12/31", formatted);
        }

        [Fact]
        public void TestFormatTimeForPicker()
        {
            var testDate = new DateTime(2019, 12, 31, 15, 0, 0);
            var formatted = testDate.FormatTimeForPicker();
            Assert.Equal("15:00", formatted);
        }
    }
}
