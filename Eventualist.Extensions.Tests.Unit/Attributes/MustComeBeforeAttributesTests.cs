using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eventualist.Extensions.Attributes;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Attributes
{
    internal class TimePeriod
    {
        [MustComeBefore("EndDate")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class MustComeBeforeAttributesTests
    {
        [Fact]
        public void TestCorrectPeriod()
        {
            var period = new TimePeriod
            {
                StartDate = DateTime.Now.Subtract(TimeSpan.FromDays(8)),
                EndDate = DateTime.Now
            };

            var context = new ValidationContext(period);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(period, context, results, true);

            Assert.True(isValid);
        }

        [Fact]
        public void TestWrongTimePeriod()
        {
            var period = new TimePeriod
            {
                StartDate = DateTime.Now.Add(TimeSpan.FromDays(8)),
                EndDate = DateTime.Now
            };

            var context = new ValidationContext(period);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(period, context, results, true);

            Assert.False(isValid);

        }

        [Fact]
        public void TestEqualDates()
        {
            var now = DateTime.Now;
            var period = new TimePeriod
            {
                StartDate = now,
                EndDate = now
            };

            var context = new ValidationContext(period);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(period, context, results, true);

            Assert.False(isValid);


        }
    }
}
