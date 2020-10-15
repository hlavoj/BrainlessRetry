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
            service.AttemptCount = 0;
            var result1 = retry.Retry<string>(10, 1, () => service.TestSuccess(1));
            service.AttemptCount = 0;
            var result2 = await retry.RetryAsync(10, 1, () => service.TestSuccessAsync(1));

            service.AttemptCount = 0;
            retry.Retry(10, 1, () => service.TestVoidSuccess(1));
            service.AttemptCount = 0;
            await retry.Retry(10, 1, () => service.TestVoidSuccessAsync(1));
            service.AttemptCount = 0;
            await retry.RetryAsync(10, 1, () => service.TestVoidSuccessAsync(1));


        }
    }
}
