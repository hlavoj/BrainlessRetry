using System;
using System.Threading;
using System.Threading.Tasks;

namespace BrainlessRetry
{
    public static class WaitAndRetry 
    {

        public static async Task<T> RetryAsync<T>(int attemptCount, int waitTime, Func<Task<T>> method)
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

        public static async Task RetryAsync(int attemptCount, int waitTime, Func<Task> method)
        {
            for (int i = 1; i <= attemptCount; i++)
            {
                try
                {
                    await method.Invoke();
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
        }

        public static T Retry<T>(int attemptCount, int waitTime, Func<T> method)
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


        public static void Retry(int attemptCount, int waitTime, Action method)
        {
            for (int i = 1; i <= attemptCount; i++)
            {
                try
                {
                    method.Invoke();
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
        }

    }
}
