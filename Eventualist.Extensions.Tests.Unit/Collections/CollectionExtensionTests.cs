using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eventualist.Extensions.Collections;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Collections
{
    public class CollectionExtensionTests
    {
        [Fact]
        public void TestEmpty()
        {
            var list = new List<string>();
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void TestEmptyOnNonEmptyList()
        {
            var list = new List<string>() { "a", "b" };
            Assert.False(list.IsEmpty());
        }

        [Fact]
        public void TestNonEmpty()
        {
            var list = new List<string>() { "a", "b" };
            Assert.True(list.IsNotEmpty());
        }

        [Fact]
        public void TestNonEmptyOnEmptyList()
        {
            var list = new List<string>();
            Assert.False(list.IsNotEmpty());
        }

        [Fact]
        public void TestDivideRegular()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6 };
            var divisions = list.Divide(3).ToList();
            Assert.True(divisions.IsNotEmpty());
            Assert.True(divisions.Count == 2);
            Assert.True(divisions[0].Count() == 3);
            Assert.True(divisions[1].Count() == 3);
        }

        [Fact]
        public void TestDivideIrregular()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            var divisions = list.Divide(3).ToList();
            Assert.True(divisions.IsNotEmpty());
            Assert.True(divisions.Count == 3);
            Assert.True(divisions[0].Count() == 3);
            Assert.True(divisions[1].Count() == 3);
            Assert.True(divisions[2].Count() == 1);
        }
    }
}
