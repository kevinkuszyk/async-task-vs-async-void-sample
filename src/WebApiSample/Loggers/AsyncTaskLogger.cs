using System.Threading.Tasks;

namespace WebApiSample.Loggers
{
    public class AsyncTaskLogger
    {
        public void Info(string message)
        {
            LogAsync(message);
        }

        private async Task LogAsync(string message)
        {
            await PostAsync(message);
        }

        private async Task PostAsync(string message)
        {
            // Real implementation will POST logs to event hub
            await Task.Run(async () => await Task.Delay(1000));
        }
    }
}