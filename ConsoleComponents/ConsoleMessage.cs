using System;

namespace ConsoleComponents
{
    public class ConsoleMessage
    {
        public static void ShowMessage(string message, bool blocking = false, string blockMessage = "")
        {
            Console.WriteLine(message);

            if (blocking)
            {
                Console.WriteLine(blockMessage);
                Console.ReadKey();
            }
        }
        
    }
}
