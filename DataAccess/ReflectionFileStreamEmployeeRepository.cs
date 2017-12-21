using System;
using System.IO;
using System.Text;
using Domain;

namespace DataAccess
{
    public class ReflectionFileStreamEmployeeRepository: FileStreamEmployeeRepository
    {
        public ReflectionFileStreamEmployeeRepository(string rootDirPath): base(rootDirPath)
        {
        }

        public override void Add(Employee obj)
        {
            //Como o id é sequencial e começa em 0, o próximo Id é igual ao número de arquivos (employees) no diretório base
            obj.Id = obj.Id < 0 ? BaseDataDirectory.GetFiles().Length : obj.Id;

            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{obj.Id}.dat"));

            using (var fstream = file.Open(FileMode.Create, FileAccess.Write))
            {
                StringBuilder recordBuilder = new StringBuilder();

                foreach (var property in obj.GetType().GetProperties())
                {
                    recordBuilder.Append($"{property.Name}: {property.GetValue(obj)}//");
                }

                var encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(recordBuilder.ToString());
                fstream.Write(bytes, 0, bytes.Length);
            }
        }

        protected override Employee Get(FileInfo file)
        {
            Employee employee = null;

            if (file.Exists)
            {
                using (var fstream = file.OpenRead())
                {
                    byte[] buffer = new byte[fstream.Length];
                    fstream.Read(buffer, 0, buffer.Length);
                    employee = DecodeEmployee(buffer);
                }
            }

            return employee;
        }

        private static Employee DecodeEmployee(byte[] encodedEmployee)
        {
            Employee employee = new Employee();
            var encoding = new UTF8Encoding();
            string[] fields = encoding.GetString(encodedEmployee).Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

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