using System;
using System.Reflection;
using Domain.Interfaces;

namespace ConsoleComponents
{ 
    public class InputText
    {
        public delegate bool InputValidator(string valueToCheck, out string errorMessage);

        private string _text;
        private InputValidator _validator;


        public InputText(string text, InputValidator validator = null)
        {
            _text = text;
            _validator = validator;
        }

        public virtual string Show()
        {
            Console.Write(_text);
            string value = Console.ReadLine();

            if (_validator != null)
            {
                string errorMessage;
                bool valid = _validator(value, out errorMessage);

                if (!valid)
                {
                    Console.WriteLine(errorMessage);
                    value = Show();
                }
            }

            return value;
        }
    }
}
