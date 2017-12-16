using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Components
{
    public class Form<T>
    {
        private T model;

        public Form(T model)
        {
            this.model = model;
        }

        public void Show()
        {
            if (model!= null)
            {
                foreach(var prop in model.GetType().GetProperties())
                {
                    Console.Write(prop.Name + ": ");
                    prop.SetValue(model, Console.ReadLine());
                }
            }
        }
    }
}
