using System;
using System.Threading.Tasks;

namespace BrainlessRetry.ConsoleExample
{
    public class Service
    {
        public int AttemptCount { get; set; }

        public string TestSuccess(int a)
        {
            if (AttemptCount == 0)
                throw new Exception();
            return a.ToString();
        }

        public async Task<string> TestSuccessAsync(int a)
        {
            if (AttemptCount == 0)
                throw new Exception();
            await Task.Delay(0);
            return a.ToString();
        }


        public void TestVoidSuccess(int a)
        {
            if (AttemptCount == 0)
                throw new Exception();
        }

        public async Task TestVoidSuccessAsync(int a)
        {
            if (AttemptCount == 0)
                throw new Exception();
            await Task.Delay(0);
        }

    }
}
