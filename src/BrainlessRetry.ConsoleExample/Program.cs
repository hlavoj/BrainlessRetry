using System;
using System.Threading.Tasks;

namespace BrainlessRetry.ConsoleExample
{
    class Program
    {

        static async Task Main(string[] args)
        {
            WaitAndRetry retry = new WaitAndRetry();
            Service service = new Service();

            #region methods with return

            // synchronous call
            service.AttemptCount = 0;
            var result1 = retry.Retry<string>(10, 1, () => service.TestSuccess(1));

            // async call (both options are correct)
            service.AttemptCount = 0;
            var result21 = await retry.RetryAsync<string>(10, 1, () => service.TestSuccessAsync(1));
            service.AttemptCount = 0;
            var result23 = await retry.RetryAsync<string>(10, 1, async () => await service.TestSuccessAsync(1));

            #endregion

            #region Void Methods

            // synchronous call
            service.AttemptCount = 0;
            retry.Retry(10, 1, () => service.TestVoidSuccess(1));

            // async call (both options are correct)
            service.AttemptCount = 0;
            await retry.RetryAsync(10, 1,  () => service.TestVoidSuccessAsync(1));
            service.AttemptCount = 0;
            await retry.RetryAsync(10, 1, async () => await service.TestVoidSuccessAsync(1));

            // This is wrong usage  !!!
            service.AttemptCount = 0;
            await retry.Retry(10, 1, () => service.TestVoidSuccessAsync(1));

            #endregion

        }
    }
}
