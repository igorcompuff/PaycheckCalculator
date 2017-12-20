using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Domain;
using Domain.Interfaces;

namespace DataAccess
{
    public class SimpleFileStreamEmployeeRepository : IRepository<Employee>, IDisposable
    {
        private DirectoryInfo _baseDataDirectory;
        private DirectoryInfo _rootDirectory;
        private int _nextId = 0;
        private readonly UTF8Encoding _encoding = new UTF8Encoding();

        ~SimpleFileStreamEmployeeRepository()
        {
            Dispose();
        }
        public SimpleFileStreamEmployeeRepository(string rootDirPath)
        {
            CreateRootDirectory(rootDirPath);
            CreateDataBaseDirectory();
            Configure();
        }

        private void CreateRootDirectory(string baseDirPath)
        {
            _rootDirectory = new DirectoryInfo(baseDirPath);

            if (!_rootDirectory.Exists)
            {
                _rootDirectory.Create();
            }
        }
        private void CreateDataBaseDirectory()
        {
            _baseDataDirectory = _rootDirectory.CreateSubdirectory("Employees");
        }
        private void Configure()
        {
            var configFile = new FileInfo(Path.Combine(_rootDirectory.FullName, "config.dat"));

            if (configFile.Exists)
            {
                using (var fstream = configFile.Open(FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[fstream.Length];
                    fstream.Read(buffer, 0, buffer.Length);
                    string decoded = _encoding.GetString(buffer);
                    _nextId = int.Parse(decoded);
                }
            }
        }

        private void SaveConfig()
        {
            var configFile = new FileInfo(Path.Combine(_rootDirectory.FullName, "config.dat"));

            using (var fstream = configFile.Open(FileMode.Create, FileAccess.Write))
            {
                var buffer = _encoding.GetBytes(_nextId.ToString());
                fstream.Write(buffer, 0, buffer.Length);
            }
        }

        public void Add(Employee obj)
        {
            obj.Id = obj.Id < 0 ? _nextId++ : obj.Id;
            var file = new FileInfo(Path.Combine(_baseDataDirectory.FullName, $"{obj.Id}.dat"));

            using (var fstream = file.Open(FileMode.Create, FileAccess.Write))
            {
                StringBuilder recordBuilder = new StringBuilder();

                foreach (var property in obj.GetType().GetProperties())
                {
                    recordBuilder.Append($"{property.Name}: {property.GetValue(obj)}//");
                }

                byte[] bytes = _encoding.GetBytes(recordBuilder.ToString());
                fstream.Write(bytes, 0, bytes.Length);
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();

            foreach (var employeeFile in _baseDataDirectory.GetFiles())
            {
                var employee = GetEmployee(employeeFile);

                if (employee != null)
                {
                    employees.Add(employee);
                }
            }

            employees.Sort((emp1, emp2) => emp1.Id > emp2.Id ? 1 : (emp1.Id == emp2.Id ? 0 : -1));
            return employees;
        }

        public Employee GetById(int id)
        {
            var file = new FileInfo(Path.Combine(_baseDataDirectory.FullName, $"{id}.dat"));
            return GetEmployee(file);
        }

        private Employee GetEmployee(FileInfo file)
        {
            Employee employee = null;
            string record;

            if (file.Exists)
            {
                employee = new Employee();
                using (var fstream = file.OpenRead())
                {
                    byte[] buffer = new byte[fstream.Length];
                    fstream.Read(buffer, 0, buffer.Length);
                    employee = DecodeEmployee(buffer);
                }
            }

            return employee;
        }

        private Employee DecodeEmployee(byte[] encodedEmployee)
        {
            Employee employee = new Employee();
            string[] fields = _encoding.GetString(encodedEmployee).Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);

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
                    propertyValue = Convert.ToInt32(fieldDecomposed[1]);

                }
                else if (property.PropertyType == typeof(double))
                {
                    propertyValue = Convert.ToDouble(fieldDecomposed[1]);
                }
                else
                {
                    propertyValue = fieldDecomposed[1];
                }

                property.SetValue(employee, propertyValue);
            }

            return employee;
        }

        public void Remove(Employee employee)
        {
            var file = new FileInfo(Path.Combine(_baseDataDirectory.FullName, $"{employee.Id}.dat"));

            if (file.Exists)
            {
                file.Delete();
            }
        }

        public void Dispose()
        {
            SaveConfig();
        }
    }
}