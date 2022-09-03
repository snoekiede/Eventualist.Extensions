using System;
using System.Collections.Generic;
using System.Text;
using Eventualist.Extensions.Booleans;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Booleans
{
    public class BooleanExtensionTests
    {
        [Fact]
        public void ShouldHaveNot()
        {
            var testvar = false;
            var result = testvar.AddNot("implemented");
            Assert.Contains("not", result);
        }

        [Fact]
        public void ShouldHaveDifferentNegation()
        {
            var testvar = false;
            var result = testvar.AddNot("geïmplementeerd", "niet");
            Assert.Contains("niet", result);
        }

        [Fact]
        public void ShouldNotHaveNegation()
        {
            var testvar = true;
            var result = testvar.AddNot("implemented");
            Assert.DoesNotContain("not", result);
        }

        [Fact]
        public void ShouldNotHaveDifferentNegation()
        {
            var testvar = true;
            var result = testvar.AddNot("geïmplementeerd", "niet");
            Assert.DoesNotContain("not", result);
        }
    }
}
