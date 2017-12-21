using System.Collections.Generic;

namespace ConsoleComponents
{
    public class Form: IComponent
    {
        private List<IComponent> inputs = new List<IComponent>();

        public void AddComponent(IComponent component)
        {
            inputs.Add(component);
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
