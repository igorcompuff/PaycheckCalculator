using System;

namespace ConsoleComponents
{
    public class ConsoleConfirmation
    {
        public string ConfirmMessage { get; set; }

        public static bool Show(string message)
        {
            Console.Write(message);
            Console.Write(" (S/N): ");
            string opt = Console.ReadLine();

            return opt.ToLower().Equals("s");
        }
    }
}