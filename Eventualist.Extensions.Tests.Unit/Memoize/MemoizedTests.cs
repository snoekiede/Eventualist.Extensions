using System;
using System.Collections.Generic;
using System.Text;
using Eventualist.Extensions.Functions;
using Xunit;
namespace Eventualist.Extensions.Tests.Unit.Memoize
{
    public class MemoizedTests
    {

        [Fact]
        public void TestSimpleFibonacci()
        {
            var result = Util.Util.Fibonacci(3);
            Assert.True(result==3);
        }

        [Fact]
        public void TestMemoize()
        {
            Func<int, int> normalFunction = Util.Util.Fibonacci;
            Func<int, int> memoizedFunction = normalFunction.Memoize();

            var normalResult = normalFunction(10);
            var memoizeResult = memoizedFunction(10);

            Assert.True(normalResult==memoizeResult);

            normalResult = normalFunction(15);
            for (int i = 0; i < 16; i++)
            {
                memoizedFunction(i);
            }

            memoizeResult = memoizedFunction(15);
            Assert.True(normalResult==memoizeResult);
        }
    }
}
