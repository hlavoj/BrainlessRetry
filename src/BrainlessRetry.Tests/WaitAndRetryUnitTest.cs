using System;
using System.Diagnostics;
using Xunit;

namespace BrainlessRetry.Tests
{
    public class WaitAndRetryUnitTest
    {
        [Fact]
        public void SuccessOnFirstTry()
        {
            IWaitAndRetry wait = new WaitAndRetry();

            var result = wait.Retry(1, 1, () => CalledMethodSuccess(1));
            Assert.Equal("1", result);
        }

        [Fact]
        public void FailOnFirstTry()
        {
            IWaitAndRetry wait = new WaitAndRetry();

            void Act() => wait.Retry(1, 1, () => CalledMethodFail(1));
            Assert.Throws<RetryException>(Act);
        }

        [Fact]
        public void SuccessOnFifthTry()
        {
            int i = 0;
            IWaitAndRetry wait = new WaitAndRetry();

            var result = wait.Retry(10, 1, () => CalledMethodSuccessOnFifthTry(1, ref i));
            Assert.Equal("1", result);
            Assert.Equal(5, i);
        }

        [Fact]
        public void FailedFourthTimes()
        {
            int i = 0;
            IWaitAndRetry wait = new WaitAndRetry();

            Action act = () => wait.Retry(4, 1, () => CalledMethodSuccessOnFifthTry(1, ref i));
            Assert.Throws<RetryException>(act);
        }

        [Fact]
        public void SuccessOnFifthTryWith_ExecutionTimeTest()
        {
            int i = 0;
            IWaitAndRetry wait = new WaitAndRetry();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = wait.Retry(10, 100, () => CalledMethodSuccessOnFifthTry(1, ref i));
            sw.Stop();
            Assert.InRange(sw.ElapsedMilliseconds, 400,500);
            Assert.Equal("1", result);
            Assert.Equal(5, i);
        }


        private string CalledMethodSuccessOnFifthTry(int a, ref int i)
        {
            if (++i < 5)
                throw new Exception();

            return a.ToString();
        }

        private  string CalledMethodSuccess(int a)
        {
            return a.ToString();
        }

        private string CalledMethodFail(int a)
        {
            throw new Exception();
        }
    }
}
