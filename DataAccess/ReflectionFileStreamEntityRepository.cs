using System;
using System.IO;
using System.Text;
using Domain;
using Domain.Interfaces;

namespace DataAccess
{
    public class ReflectionFileStreamEntityRepository: BaseFileStreamRepository<Employee>
    {
        public ReflectionFileStreamEntityRepository(string rootDirPath): base(rootDirPath)
        {
        }

        protected override void Save(FileStream fs, Employee obj)
        {
            StringBuilder recordBuilder = new StringBuilder();

            foreach (var property in obj.GetType().GetProperties())
            {
                recordBuilder.Append($"{property.Name}: {property.GetValue(obj)}//");
            }

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(recordBuilder.ToString());
            fs.Write(bytes, 0, bytes.Length);
        }

        protected override Employee GetEntity(FileStream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            return Decode(buffer);
        }

        private static Employee Decode(byte[] encoded)
        {
            Employee employee = new Employee();
            var encoding = new UTF8Encoding();
            string[] fields = encoding.GetString(encoded).Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fields)
            {
                string[] fieldDecomposed = field.Split(':');

                var property = employee.GetType().GetProperty(fieldDecomposed[0]);

                if (property == null)
                {
                    throw new InvalidOperationException("The database is corrupted");
                }

                object propertyValue;

                if (property.PropertyType == typeof(int))
                {
                    propertyValue = int.Parse(fieldDecomposed[1]);

                }
                else if (property.PropertyType == typeof(double))
                {
                    propertyValue = double.Parse(fieldDecomposed[1]);
                }
                else
                {
                    propertyValue = fieldDecomposed[1];
                }

                property.SetValue(employee, propertyValue);
            }

            return employee;
        }
    }
}