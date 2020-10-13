namespace BrainlessRetry.ConsoleExample
{
    class Program
    {

        static void Main(string[] args)
        {
            IWaitAndRetry retry = new WaitAndRetry();
            Service service = new Service();
            var result = retry.Retry<string>(1, 1, () => service.TestSuccess(1));
        }
    }
}
