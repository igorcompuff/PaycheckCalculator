using System;
using System.Collections.Generic;

namespace View.Components
{
    public class ConsoleMenu
    {
        public class ConsoleMenuOption
        {
            public string OptionCode { get; private set; }
            public string OptionDescription { get; private set; }
            private Action optionAction;
            private bool infinity;

            public ConsoleMenuOption(string optionCode, string optionDescription, Action optionAction = null)
            {
                OptionCode = optionCode;
                OptionDescription = optionDescription;
                this.optionAction = optionAction;
            }

            public void Execute()
            {
                optionAction?.Invoke();
            }
        }

        private bool infinity;
        public string RequestOptionMessage { get; set; }
        public string OptionNotFoundMessage { get; set; }

        private Dictionary<string, ConsoleMenuOption> options;

        public ConsoleMenu(bool infinity)
        {
            this.infinity = infinity;
            options = new Dictionary<string, ConsoleMenuOption>();
        }

        public void AddOption(ConsoleMenuOption option)
        {
            options.Add(option.OptionCode, option);
        }

        public void AddExitOption(ConsoleMenuOption option)
        {
            AddOption(new ConsoleMenuOption(option.OptionCode, option.OptionDescription, () => infinity = false));
        }

        public void Show()
        {
            do
            {
                Console.Clear();
                foreach (var opt in options)
                {
                    Console.WriteLine($"{opt.Value.OptionCode} - {opt.Value.OptionDescription}");
                }
                Console.Write((RequestOptionMessage + ": ") ?? "Enter the option for the operation: ");
                var selected = Console.ReadLine();

                if (options.ContainsKey(selected))
                {
                    options[selected].Execute();
                }
                else
                {
                    Console.WriteLine(OptionNotFoundMessage ?? "Operation invalid.");
                    Console.Write("Press any key to continue ...");
                    Console.ReadKey();
                }

            } while (infinity);
        }

    }
}
