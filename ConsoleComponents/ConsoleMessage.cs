using System;

namespace ConsoleComponents
{
    public class ConsoleMessage: IComponent
    {
        private readonly string _message;
        private readonly bool _blocking;

        public ConsoleMessage(string message, bool blocking = false)
        {
            _message = message;
            _blocking = blocking;
        }

        public void Show()
        {
            Console.WriteLine(_message);

            if (_blocking)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        
    }
}
