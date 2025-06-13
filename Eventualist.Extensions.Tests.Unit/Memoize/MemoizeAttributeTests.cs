using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eventualist.Extensions.Functions;
using Xunit;

namespace Eventualist.Extensions.Tests.Unit.Memoize
{
    public class MemoizeAttributeTests
    {
        public class MemoizeTestClass
        {
            public int CallCount { get; private set; }

            [Memoize]
            public virtual int NoParams()
            {
                CallCount++;
                return 42;
            }

            [Memoize]
            public virtual int Add(int a, int b)
            {
                CallCount++;
                return a + b;
            }

            [Memoize]
            public virtual int SlowAdd(int a, int b)
            {
                CallCount++;
                Thread.Sleep(50);
                return a + b;
            }

            public void ResetCallCount()
            {
                CallCount = 0;
            }

            private static int _staticCallCount;
            public static int StaticCallCount => _staticCallCount;

            public static void ResetStaticCallCount() => _staticCallCount = 0;

            [Memoize]
            public static int StaticSlowAdd(int a, int b)
            {
                Interlocked.Increment(ref _staticCallCount);
                Thread.Sleep(50);
                return a + b;
            }
        }



        //because the AspectInjector package has trouble with test frameworks, the code needs to call the attribute directly here.


        public class TestMemoizeImplementation
        {
            private static readonly MemoizeAttribute _memoizeImpl = new MemoizeAttribute();
            private static int _callCount;

            public static void ResetCallCount() => _callCount = 0;
            public static int CallCount => _callCount;

            public static int MemoizedAdd(int a, int b)
            {
                return (int)_memoizeImpl.Handle(
                    typeof(TestMemoizeImplementation).GetMethod(nameof(MemoizedAdd)),
                    new object[] { a, b },
                    args => {
                        Interlocked.Increment(ref _callCount);
                        Thread.Sleep(50);
                        return ((int)args[0]) + ((int)args[1]);
                    });
            }
        }

        [Fact]
        public void TestMemoizeImplementation_IsCaching()
        {
            TestMemoizeImplementation.ResetCallCount();
            
            var result1 = TestMemoizeImplementation.MemoizedAdd(2, 3);
            var result2 = TestMemoizeImplementation.MemoizedAdd(2, 3);
            
            Assert.Equal(5, result1);
            Assert.Equal(5, result2);
            Assert.Equal(1, TestMemoizeImplementation.CallCount);
        }
        
        [Fact]
        public void TestMemoizeImplementation_IsThreadSafe()
        {
            TestMemoizeImplementation.ResetCallCount();
            
            Parallel.For(0, 10, _ => {
                var result = TestMemoizeImplementation.MemoizedAdd(10, 20);
                Assert.Equal(30, result);
            });
            
            Assert.Equal(1, TestMemoizeImplementation.CallCount);
        }
    }
}