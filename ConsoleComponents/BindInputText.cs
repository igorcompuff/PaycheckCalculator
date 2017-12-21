using System;
using System.Reflection;

namespace ConsoleComponents
{
    public class BindInputText<T>: InputText
    {
        private readonly T _model;
        private readonly PropertyInfo _property;

        public BindInputText(string label, T model, string propertyName, InputValidator validator):base(label, validator)
        {
            if (model == null || string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("O model e/ou a propriedade informada não são válidos");
            }

            _property = model.GetType().GetProperty(propertyName);
            _model = model;
        }

        public override void Show()
        {
            base.Show();
            _property.SetValue(_model, InputData);
        }
    }
}