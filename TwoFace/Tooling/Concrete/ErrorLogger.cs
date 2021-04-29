using System;
using TwoFace.Tooling.Abstract;

namespace TwoFace.Tooling.Concrete
{
    public class ErrorLogger : IErrorLogger
    {
        public void LogError(Exception ex, string infoMessage)
        {
#if DEBUG
            // Print out to the console
            Console.WriteLine("Uh oh! Something happened");
            Console.WriteLine($"Exception message: {ex.Message}");
            Console.WriteLine($"Info message: {infoMessage}");
#else
            // Or use whatever logging mechanism you'd like
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info(ex, infoMessage);
#endif
        }
    }
}
