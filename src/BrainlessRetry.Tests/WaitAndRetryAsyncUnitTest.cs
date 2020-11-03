using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace BrainlessRetry.Tests
{
    public class WaitAndRetryAsyncUnitTest
    {
        #region async methods with return

        [Fact]
        public async void Async_SuccessOnFirstTry()
        {
            var result = await WaitAndRetry.RetryAsync(1, 1, () => CalledMethodSuccessAsync(1));
            Assert.Equal("1", result);
        }

        [Fact]
        public async void Async_FailOnFirstTry()
        {
            async Task Act() => await WaitAndRetry.RetryAsync(1, 1, () => CalledMethodFailAsync(1));
            await Assert.ThrowsAsync<RetryException>(Act);
        }

        [Fact]
        public async void Async_SuccessOnFifthTry()
        {
            int i = 0;
            var result = await WaitAndRetry.RetryAsync(10, 1, () => CalledMethodSuccessOnFifthTryAsync(1));
            Assert.Equal("1", result);
            Assert.Equal(5, i);

            async Task<string> CalledMethodSuccessOnFifthTryAsync(int a)
            {
                await Task.Delay(0);
                if (++i < 5)
                    throw new Exception();

                return a.ToString();
            }
        }

        [Fact]
        public async void Async_FailedFourthTimes()
        {
            int i = 0;
            Func<Task> act = async () => await WaitAndRetry.RetryAsync(4, 1, () => CalledMethodSuccessOnFifthTryAsync(1));
            await Assert.ThrowsAsync<RetryException>(act);
            Assert.Equal(4, i);

            async Task<string> CalledMethodSuccessOnFifthTryAsync(int a)
            {
                await Task.Delay(0);
                if (++i < 5)
                    throw new Exception();

                return a.ToString();
            }
        }

        [Fact]
        public async void Async_SuccessOnFifthTryWith_ExecutionTimeTest()
        {
            int i = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = await WaitAndRetry.RetryAsync(10, 100, () => CalledMethodSuccessOnFifthTryAsync(1));
            sw.Stop();
            Assert.InRange(sw.ElapsedMilliseconds, 400, 500);
            Assert.Equal("1", result);
            Assert.Equal(5, i);

            async Task<string> CalledMethodSuccessOnFifthTryAsync(int a)
            {
                await Task.Delay(0);
                if (++i < 5)
                    throw new Exception();

                return a.ToString();
            }
        }

        #endregion

        #region async void methods 

        [Fact]
        public async void AsyncVoid_SuccessOnFirstTry()
        {
            await WaitAndRetry.RetryAsync(1, 1, () => CalledVoidMethodSuccessAsync(1));
        }

        [Fact]
        public async void AsyncVoid_FailOnFirstTry()
        {
            async Task Act() => await WaitAndRetry.RetryAsync(1, 1, () => CalledVoidMethodFailAsync(1));
            await Assert.ThrowsAsync<RetryException>(Act);
        }

        [Fact]
        public async void AsyncVoid_SuccessOnFifthTry()
        {
            int i = 0;
            await WaitAndRetry.RetryAsync(10, 1, () => CalledVoidMethodSuccessOnFifthTryAsync(1));
            Assert.Equal(5, i);

            async Task CalledVoidMethodSuccessOnFifthTryAsync(int a)
            {
                await Task.Delay(0);
                if (++i < 5)
                    throw new Exception();
            }
        }

        [Fact]
        public async void AsyncVoid_FailedFourthTimes()
        {
            int i = 0;
            Func<Task> act = async () => await WaitAndRetry.RetryAsync(4, 1, () => CalledVoidMethodSuccessOnFifthTryAsync(1));
            await Assert.ThrowsAsync<RetryException>(act);
            Assert.Equal(4, i);

            async Task CalledVoidMethodSuccessOnFifthTryAsync(int a)
            {
                await Task.Delay(0);
                if (++i < 5)
                    throw new Exception();
            }
        }

        [Fact]
        public async void AsyncVoid_SuccessOnFifthTryWith_ExecutionTimeTest()
        {
            int i = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await WaitAndRetry.RetryAsync(10, 100, () => CalledVoidMethodSuccessOnFifthTryAsync(1));
            sw.Stop();
            Assert.InRange(sw.ElapsedMilliseconds, 400, 500);
            Assert.Equal(5, i);

            async Task CalledVoidMethodSuccessOnFifthTryAsync(int a)
            {
                await Task.Delay(0);
                if (++i < 5)
                    throw new Exception();
            }
        }

        #endregion

        #region private methods


        private async Task<string> CalledMethodSuccessAsync(int a)
        {
            await Task.Delay(0);
            return a.ToString();
        }

        private async Task<string> CalledMethodFailAsync(int a)
        {
            await Task.Delay(0);
            throw new Exception();
        }

        private async Task CalledVoidMethodSuccessAsync(int a)
        {
            await Task.Delay(0);
            return;
        }

        private async Task CalledVoidMethodFailAsync(int a)
        {
            await Task.Delay(0);
            throw new Exception();
        }
        #endregion



    }
}
