using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrainlessRetry
{
    public class WaitAndRetry : IWaitAndRetry
    {
        public async Task<T> RetryAsync<T>(int attemptCount, int waitTime, Func<Task<T>> method)
        {
            T result = default;
            for (int i = 1; i <= attemptCount; i++)
            {
                try
                {
                    result = await method.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    if (i == attemptCount)
                    {
                       throw new RetryException($"Retrying method: '{method.Method.Name}' failed for {attemptCount}times. See inner exception for last result.", e);
                    }
                    await Task.Delay(waitTime);
                }
            }
            return result;
        }

        public T Retry<T>(int attemptCount, int waitTime, Func<T> method)
        {
            T result = default;
            for (int i = 1; i <= attemptCount; i++)
            {
                try
                {
                    result = method.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    if (i == attemptCount)
                    {
                       throw new RetryException($"Retrying method: '{method.Method.Name}' failed for {attemptCount}times. See inner exception for last result.", e);
                    }

                    Thread.Sleep(waitTime);
                }
            }
            return result;
        }
    }
}
