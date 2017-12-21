using System;
using System.Reflection;
using Domain.Interfaces;

namespace ConsoleComponents
{ 
    public class InputText: IComponent
    {
        public delegate bool InputValidator(string valueToCheck, out string errorMessage);

        private readonly string _label;
        private readonly InputValidator _validator;

        public string InputData { get; set; }

        public InputText(string label, InputValidator validator = null)
        {
            _label = label;
            _validator = validator;
        }

        public virtual void Show()
        {
            Console.Write(_label);
            var value = Console.ReadLine();

            if (_validator != null)
            {
                bool valid = _validator(value, out var errorMessage);

                if (!valid)
                {
                    Console.WriteLine(errorMessage);
                    Show();
                }
                else
                {
                    InputData = value;
                }
            }
        }
    }
}
