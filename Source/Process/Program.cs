using PipServicesLimitsDotnet.Container;

using System;

namespace Process
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = (new LimitsProcess()).RunAsync(args);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
