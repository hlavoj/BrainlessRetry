using System;
using System.Threading.Tasks;

namespace BrainlessRetry
{
    public interface IWaitAndRetry
    {
        Task<T> RetryAsync<T>(int attemptCount, int waitTime, Func<Task<T>> method);
        T Retry<T>(int attemptCount, int waitTime, Func<T> method);
    }
}