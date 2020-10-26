using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace BrainlessRetry.Tests
{
    public class WaitAndRetryUnitTest
    {

        #region sync methods with return

        [Fact]
        public void Sync_SuccessOnFirstTry()
        {
            var result = WaitAndRetry.Retry(1, 1, () => CalledMethodSuccess(1));
            Assert.Equal("1", result);
        }

        [Fact]
        public void Sync_FailOnFirstTry()
        {
            void Act() => WaitAndRetry.Retry(1, 1, () => CalledMethodFail(1));
            Assert.Throws<RetryException>(Act);
        }

        [Fact]
        public void Sync_SuccessOnFifthTry()
        {
            int i = 0;
            var result = WaitAndRetry.Retry(10, 1, () => CalledMethodSuccessOnFifthTry(1, ref i));
            Assert.Equal("1", result);
            Assert.Equal(5, i);
        }

        [Fact]
        public void Sync_FailedFourthTimes()
        {
            int i = 0;
            Action act = () => WaitAndRetry.Retry(4, 1, () => CalledMethodSuccessOnFifthTry(1, ref i));
            Assert.Throws<RetryException>(act);
            Assert.Equal(4, i);

        }

        [Fact]
        public void Sync_SuccessOnFifthTryWith_ExecutionTimeTest()
        {
            int i = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = WaitAndRetry.Retry(10, 100, () => CalledMethodSuccessOnFifthTry(1, ref i));
            sw.Stop();
            Assert.InRange(sw.ElapsedMilliseconds, 400,500);
            Assert.Equal("1", result);
            Assert.Equal(5, i);
        }

        #endregion

        #region sync void methods 

        [Fact]
        public void SyncVoid_SuccessOnFirstTry()
        {
            WaitAndRetry.Retry(1, 1, () => CalledVoidMethodSuccess(1));
        }

        [Fact]
        public void SyncVoid_FailOnFirstTry()
        {
            void Act() => WaitAndRetry.Retry(1, 1, () => CalledVoidMethodFail(1));
            Assert.Throws<RetryException>(Act);
        }

        [Fact]
        public void SyncVoid_SuccessOnFifthTry()
        {
            int i = 0;
            WaitAndRetry.Retry(10, 1, () => CalledVoidMethodSuccessOnFifthTry(1, ref i));
            Assert.Equal(5, i);
        }

        [Fact]
        public void SyncVoid_FailedFourthTimes()
        {
            int i = 0;
            Action act = () => WaitAndRetry.Retry(4, 1, () => CalledVoidMethodSuccessOnFifthTry(1, ref i));
            Assert.Throws<RetryException>(act);
            Assert.Equal(4, i);

        }

        [Fact]
        public void SyncVoid_SuccessOnFifthTryWith_ExecutionTimeTest()
        {
            int i = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            WaitAndRetry.Retry(10, 100, () => CalledVoidMethodSuccessOnFifthTry(1, ref i));
            sw.Stop();
            Assert.InRange(sw.ElapsedMilliseconds, 400, 500);
            Assert.Equal(5, i);
        }

        #endregion


        #region private methods

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

        private void CalledVoidMethodSuccessOnFifthTry(int a, ref int i)
        {
            if (++i < 5)
                throw new Exception();

        }

        private void CalledVoidMethodSuccess(int a)
        {
            return;
        }

        private void CalledVoidMethodFail(int a)
        {
            throw new Exception();
        }

        #endregion



    }
}
