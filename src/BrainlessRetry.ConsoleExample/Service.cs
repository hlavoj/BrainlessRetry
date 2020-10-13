using System;

namespace BrainlessRetry.ConsoleExample
{
    public class Service
    {
        
        public string TestSuccess(int a)
        {
            return a.ToString();
        }

        public string TestFail(int a)
        {
            throw new Exception();
        }
    }
}
