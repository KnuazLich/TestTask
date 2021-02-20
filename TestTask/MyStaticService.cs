using System;
using System.Threading.Tasks;

namespace TestTask
{
    public class MyStaticService
    {
        public static async Task<int> TaskDelay()
        {
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            return 0;
        }
        public static async Task<int> DoNothing()
        {
            return 0;
        }
    }
}
