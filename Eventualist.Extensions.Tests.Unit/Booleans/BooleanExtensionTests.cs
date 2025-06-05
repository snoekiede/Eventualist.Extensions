using Eventualist.Extensions.Booleans;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Booleans
{
    public class BooleanExtensionTests
    {
        [Fact]
        public void ShouldHaveNot()
        {
            
            var result = false.AddNot("implemented");
            Assert.Contains("not", result);
        }

        [Fact]
        public void ShouldHaveNotNullable()
        {
            bool? nullableBool = null;
            var result = nullableBool.AddNot("implemented");
            Assert.Contains("unknown if implemented", result);
        }

        [Fact]
        public void ShouldHaveDifferentNegation()
        {
            
            var result = false.AddNot("implemented", "not");
            Assert.Contains("not", result);
        }

        [Fact]
        public void ShouldNotHaveNegation()
        {
            
            var result = true.AddNot("implemented");
            Assert.DoesNotContain("not", result);
        }

        [Fact]
        public void ShouldNotHaveDifferentNegation()
        {
            
            var result = true.AddNot("geïmplementeerd", "niet");
            Assert.DoesNotContain("not", result);
        }

        [Fact]
        public void YesOrNoWorksForYes()
        {
            var result = true.ToYesOrNo();
            Assert.Equal(result,"yes");
        }

        [Fact]
        public void YesOrNoWorksForNo()
        {
            var result = false.ToYesOrNo();
            Assert.Equal("no",result);
        }

        [Fact]
        public void YesOrNoWorksForNullValue()
        {
            bool? nullableBool = null;
            var result = nullableBool.ToYesOrNo("yes", "no", "unknown");
            Assert.Equal("unknown", result);
        }

        [Fact]
        public void YesOrNoWorksForYesWithOtherValue()
        {
            var result = true.ToYesOrNo("ja");
            Assert.Equal("ja",result);
        }

        [Fact]
        public void YesOrNoWorksForNoWithOtherValue()
        {
            var result = false.ToYesOrNo("ja", "nee");
            Assert.Equal("nee",result);
        }
    }
}
