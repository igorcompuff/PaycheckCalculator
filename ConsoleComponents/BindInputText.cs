using System;
using System.Reflection;

namespace ConsoleComponents
{
    public class BindInputText<T>: InputText
    {
        private T _model;
        private PropertyInfo _property;

        public BindInputText(string text, T model, string propertyName, InputValidator validator):base(text, validator)
        {
            if (model == null || string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("O model e/ou a propriedade informada não são válidos");
            }

            _property = model.GetType().GetProperty(propertyName);
            _model = model;
        }

        public override string Show()
        {
            string value = base.Show();
            _property.SetValue(_model, value);
            return value;
        }
    }
}