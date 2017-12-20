using System.Collections.Generic;

namespace ConsoleComponents
{
    public class Form<T>
    {
        private List<InputText> inputs = new List<InputText>();

        public void AddInput(InputText input)
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
