using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventualist.Extensions.Functions;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Functions
{
    public class MemoizeTests
    {
        int noParams() => 42;
        int oneParam(int first) => first + 42;
        int twoParams(int first, int second) => first + second;
        int threeParams(int first, int second, int third) => first + second + third;
        int fourParams(int first, int second, int third, int fourth) => first + second + third + fourth;

        int fiveParams(int first, int second, int third, int fourth, int fifth) =>
            first + second + third + fourth + fifth;

        int sixParams(int first, int second, int third, int fourth, int fifth, int sixth) =>
            first + second + third + fourth + fifth + sixth;

        [Fact]
        public void TestNoParams()
        {

            Func<int> testfunction = noParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction();

            Assert.Equal(42, result);


        }

        [Fact]
        public void TestOneParam()
        {
            Func<int, int> testfunction = oneParam;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(3);

            Assert.Equal(45, result);
        }

        [Fact]
        public void TestTwoParams()
        {
            Func<int, int, int> testfunction = twoParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2);

            Assert.Equal(3, result);
        }

        [Fact]
        public void TestThreeParams()
        {
            Func<int, int, int, int> testfunction = threeParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3);

            Assert.Equal(6, result);
        }

        [Fact]
        public void TestFourParams()
        {
            Func<int, int, int, int, int> testfunction = fourParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3, 4);

            Assert.Equal(10, result);
        }

        [Fact]
        public void TestFiveParams()
        {
            Func<int, int, int, int, int, int> testfunction = fiveParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3, 4, 5);

            Assert.Equal(15, result);
        }

        [Fact]
        public void TestSixParams()
        {
            Func<int, int, int, int, int, int, int> testfunction = sixParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3, 4, 5, 6);

            Assert.Equal(21, result);
        }

        
    }
}
