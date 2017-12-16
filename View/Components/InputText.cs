using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace View.Components
{
    public class InputText<T>
    {
        private T model;
        private PropertyInfo propertyBind;
        private string text;
        public InputText(T model, PropertyInfo propertyBind, string text)
        {
            this.model = model;
            this.propertyBind = propertyBind;
            this.text = text;
        }

        public void Show()
        {
            Console.Write(text);
            propertyBind.SetValue(model, Console.ReadLine());
        }
    }
}
