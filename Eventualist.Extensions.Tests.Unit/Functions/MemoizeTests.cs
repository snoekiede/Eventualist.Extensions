using System;
using Eventualist.Extensions.Functions;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Functions
{
    public class MemoizeTests
    {
        int NoParams() => 42;
        int OneParam(int first) => first + 42;
        int TwoParams(int first, int second) => first + second;
        int ThreeParams(int first, int second, int third) => first + second + third;
        int FourParams(int first, int second, int third, int fourth) => first + second + third + fourth;

        int FiveParams(int first, int second, int third, int fourth, int fifth) =>
            first + second + third + fourth + fifth;

        int SixParams(int first, int second, int third, int fourth, int fifth, int sixth) =>
            first + second + third + fourth + fifth + sixth;

        [Fact]
        public void TestNoParams()
        {

            Func<int> testfunction = NoParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction();

            Assert.Equal(42, result);


        }

        [Fact]
        public void TestOneParam()
        {
            Func<int, int> testfunction = OneParam;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(3);

            Assert.Equal(45, result);
        }

        [Fact]
        public void TestTwoParams()
        {
            Func<int, int, int> testfunction = TwoParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2);

            Assert.Equal(3, result);
        }

        [Fact]
        public void TestThreeParams()
        {
            Func<int, int, int, int> testfunction = ThreeParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3);

            Assert.Equal(6, result);
        }

        [Fact]
        public void TestFourParams()
        {
            Func<int, int, int, int, int> testfunction = FourParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3, 4);

            Assert.Equal(10, result);
        }

        [Fact]
        public void TestFiveParams()
        {
            Func<int, int, int, int, int, int> testfunction = FiveParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3, 4, 5);

            Assert.Equal(15, result);
        }

        [Fact]
        public void TestSixParams()
        {
            Func<int, int, int, int, int, int, int> testfunction = SixParams;
            var memoizeFunction = testfunction.Memoize();

            var result = memoizeFunction(1, 2, 3, 4, 5, 6);

            Assert.Equal(21, result);
        }

        
    }
}
