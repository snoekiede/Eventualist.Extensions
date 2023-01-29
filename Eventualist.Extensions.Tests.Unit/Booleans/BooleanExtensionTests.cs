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
        public void ShouldHaveDifferentNegation()
        {
            
            var result = false.AddNot("geïmplementeerd", "niet");
            Assert.Contains("niet", result);
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
