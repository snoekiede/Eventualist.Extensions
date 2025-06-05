using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eventualist.Extensions.Functions;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Functions
{
    public class MemoizeTests
    {
        private int _callCount;

        // Test functions with call counting
        int NoParams() { _callCount++; return 42; }
        int OneParam(int first) { _callCount++; return first + 42; }
        int TwoParams(int first, int second) { _callCount++; return first + second; }
        int ThreeParams(int first, int second, int third) { _callCount++; return first + second + third; }
        int FourParams(int first, int second, int third, int fourth) { _callCount++; return first + second + third + fourth; }
        int FiveParams(int first, int second, int third, int fourth, int fifth) { _callCount++; return first + second + third + fourth + fifth; }
        int SixParams(int first, int second, int third, int fourth, int fifth, int sixth) { _callCount++; return first + second + third + fourth + fifth + sixth; }

        private void ResetCallCount() => _callCount = 0;

        #region Basic Function Tests

        [Fact]
        public void TestNoParams()
        {
            ResetCallCount();
            Func<int> testFunction = NoParams;
            var memoizedFunction = testFunction.Memoize();

            // First call
            var result1 = memoizedFunction();
            Assert.Equal(42, result1);
            Assert.Equal(1, _callCount);

            // Second call should use cached result
            var result2 = memoizedFunction();
            Assert.Equal(42, result2);
            Assert.Equal(1, _callCount); // Count should still be 1
        }

        [Fact]
        public void TestOneParam()
        {
            ResetCallCount();
            Func<int, int> testFunction = OneParam;
            var memoizedFunction = testFunction.Memoize();

            // First call
            var result1 = memoizedFunction(3);
            Assert.Equal(45, result1);
            Assert.Equal(1, _callCount);

            // Same param should use cache
            var result2 = memoizedFunction(3);
            Assert.Equal(45, result2);
            Assert.Equal(1, _callCount);

            // Different param should recalculate
            var result3 = memoizedFunction(5);
            Assert.Equal(47, result3);
            Assert.Equal(2, _callCount);
        }

        [Fact]
        public void TestTwoParams()
        {
            ResetCallCount();
            Func<int, int, int> testFunction = TwoParams;
            var memoizedFunction = testFunction.Memoize();

            var result1 = memoizedFunction(1, 2);
            Assert.Equal(3, result1);
            Assert.Equal(1, _callCount);

            // Same params should use cache
            var result2 = memoizedFunction(1, 2);
            Assert.Equal(3, result2);
            Assert.Equal(1, _callCount);

            // Different params should recalculate
            var result3 = memoizedFunction(2, 3);
            Assert.Equal(5, result3);
            Assert.Equal(2, _callCount);
        }

        [Fact]
        public void TestThreeParams()
        {
            ResetCallCount();
            Func<int, int, int, int> testFunction = ThreeParams;
            var memoizedFunction = testFunction.Memoize();

            var result = memoizedFunction(1, 2, 3);
            Assert.Equal(6, result);

            // Call again with same params
            result = memoizedFunction(1, 2, 3);
            Assert.Equal(6, result);
            Assert.Equal(1, _callCount);
        }

        [Fact]
        public void TestFourParams()
        {
            ResetCallCount();
            Func<int, int, int, int, int> testFunction = FourParams;
            var memoizedFunction = testFunction.Memoize();

            var result = memoizedFunction(1, 2, 3, 4);
            Assert.Equal(10, result);

            // Call again with same params
            result = memoizedFunction(1, 2, 3, 4);
            Assert.Equal(10, result);
            Assert.Equal(1, _callCount);
        }

        [Fact]
        public void TestFiveParams()
        {
            ResetCallCount();
            Func<int, int, int, int, int, int> testFunction = FiveParams;
            var memoizedFunction = testFunction.Memoize();

            var result = memoizedFunction(1, 2, 3, 4, 5);
            Assert.Equal(15, result);

            // Call again with same params
            result = memoizedFunction(1, 2, 3, 4, 5);
            Assert.Equal(15, result);
            Assert.Equal(1, _callCount);
        }

        [Fact]
        public void TestSixParams()
        {
            ResetCallCount();
            Func<int, int, int, int, int, int, int> testFunction = SixParams;
            var memoizedFunction = testFunction.Memoize();

            var result = memoizedFunction(1, 2, 3, 4, 5, 6);
            Assert.Equal(21, result);

            // Call again with same params
            result = memoizedFunction(1, 2, 3, 4, 5, 6);
            Assert.Equal(21, result);
            Assert.Equal(1, _callCount);
        }

        #endregion

        #region Custom Equality Comparer Tests

        [Fact]
        public void TestWithCustomEqualityComparer()
        {
            ResetCallCount();

            // Case insensitive string comparer
            var comparer = StringComparer.OrdinalIgnoreCase;
            Func<string, string> stringFunc = s => { _callCount++; return s.ToUpper(); };

            var memoizedFunction = stringFunc.Memoize(comparer);

            // First call
            var result1 = memoizedFunction("test");
            Assert.Equal("TEST", result1);
            Assert.Equal(1, _callCount);

            // Different case should use cache with our comparer
            var result2 = memoizedFunction("TEST");
            Assert.Equal("TEST", result2);
            Assert.Equal(1, _callCount); // Should not increment

            // Different string should recalculate
            var result3 = memoizedFunction("other");
            Assert.Equal("OTHER", result3);
            Assert.Equal(2, _callCount);
        }

        #endregion

        #region MemoizeWithExpiration Tests

        [Fact]
        public void TestMemoizeWithExpiration()
        {
            ResetCallCount();

            Func<int, int> testFunction = OneParam;
            var shortExpiration = TimeSpan.FromMilliseconds(50);
            var memoizedFunction = testFunction.MemoizeWithExpiration(shortExpiration);

            // First call
            var result1 = memoizedFunction(5);
            Assert.Equal(47, result1);
            Assert.Equal(1, _callCount);

            // Immediate second call should use cache
            var result2 = memoizedFunction(5);
            Assert.Equal(47, result2);
            Assert.Equal(1, _callCount);

            // Wait for cache to expire
            Thread.Sleep(100);

            // Call after expiration should recalculate
            var result3 = memoizedFunction(5);
            Assert.Equal(47, result3);
            Assert.Equal(2, _callCount);
        }

        #endregion

        #region Compose Tests

        [Fact]
        public void TestCompose()
        {
            Func<int, int> addFive = x => x + 5;
            Func<int, string> toString = x => $"Number: {x}";

            var composed = addFive.Compose(toString);

            var result = composed(10);
            Assert.Equal("Number: 15", result);
        }

        [Fact]
        public void TestComposeWithMemoize()
        {
            ResetCallCount();

            Func<int, int> addFive = x => { _callCount++; return x + 5; };
            Func<int, string> toString = x => $"Number: {x}";

            var composed = addFive.Compose(toString);
            var memoized = composed.Memoize();

            // First call
            var result1 = memoized(10);
            Assert.Equal("Number: 15", result1);
            Assert.Equal(1, _callCount);

            // Second call with same input
            var result2 = memoized(10);
            Assert.Equal("Number: 15", result2);
            Assert.Equal(1, _callCount); // Should not change
        }

        #endregion

        #region Thread Safety Tests

        [Fact]
        public void TestThreadSafety()
        {
            ResetCallCount();

            Func<int, int> slowFunction = x => {
                _callCount++;
                Thread.Sleep(50); // Slow function to increase chance of race conditions
                return x * 2;
            };

            var memoizedFunction = slowFunction.Memoize();

            // Parallel execution of the same function with the same parameter
            Parallel.ForEach(Enumerable.Repeat(5, 10), _ => {
                var result = memoizedFunction(10);
                Assert.Equal(20, result);
            });

            // Should only be calculated once
            Assert.Equal(1, _callCount);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        public void TestNullFunctionThrowsException()
        {
            Func<int, int> nullFunc = null!;

            Assert.Throws<ArgumentNullException>(() => nullFunc.Memoize());
        }

        [Fact]
        public void TestComposeWithNullFunctionsThrowsException()
        {
            Func<int, int> validFunc = x => x * 2;
            Func<int, string> nullFunc = null!;

            Assert.Throws<ArgumentNullException>(() => validFunc.Compose(nullFunc));
            Assert.Throws<ArgumentNullException>(() => nullFunc.Compose(x => x.ToString()));
        }

        #endregion
    }
}