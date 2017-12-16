using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Components
{
    public class Form<T>
    {
        private List<InputText<T>> inputs = new List<InputText<T>>();

        public void AddInput(InputText<T> input)
        {
            inputs.Add(input);
        }

        public void Show()
        {
            foreach (var input in inputs)
            {
                input.Show();
            }
        }
    }
}
