using System;

namespace ConsoleComponents
{
    public class ConsoleConfirmation: IComponent
    {
        private readonly string _message;
        public bool Confirmed { get; set; }

        public ConsoleConfirmation(string message)
        {
            _message = message;
        }
        public void Show()
        {
            Console.Write(_message);
            Console.Write(" (S/N): ");
            string opt = Console.ReadLine();

            Confirmed = opt?.ToLower().Equals("s") ?? false;
        }
    }
}