using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Domain;
using Domain.Interfaces;

namespace DataAccess
{
    public class SerializationFileStreamEmployeeRepository : IRepository<Employee>, IDisposable
    {
        [Serializable]
        private class Configuration
        {
            public int NextId { get; set; }
        }
        private DirectoryInfo _baseDataDirectory;
        private DirectoryInfo _rootDirectory;
        private int _nextId = 0;
        private Configuration _config = new Configuration();

        private BinaryFormatter formatter = new BinaryFormatter();

        ~SerializationFileStreamEmployeeRepository()
        {
            Dispose();
        }
        public SerializationFileStreamEmployeeRepository(string rootDirPath)
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
                    _config = (Configuration)formatter.Deserialize(fstream);
                }
            }
        }

        private void SaveConfig()
        {
            var configFile = new FileInfo(Path.Combine(_rootDirectory.FullName, "config.dat"));

            using (var fstream = configFile.Open(FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fstream, _config);
            }
        }

        public void Add(Employee obj)
        {
            obj.Id = obj.Id < 0 ? _config.NextId++ : obj.Id;
            var file = new FileInfo(Path.Combine(_baseDataDirectory.FullName, $"{obj.Id}.dat"));

            using (var fstream = file.Open(FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(fstream, obj);
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

        public void Remove(Employee obj)
        {
            var file = new FileInfo(Path.Combine(_baseDataDirectory.FullName, $"{obj.Id}.dat"));

            if (file.Exists)
            {
                file.Delete();
            }
        }

        public void Dispose()
        {
            SaveConfig();
        }

        private Employee GetEmployee(FileInfo file)
        {
            Employee employee = null;
            string record;

            if (file.Exists)
            {
                using (var fstream = file.OpenRead())
                {
                    employee = (Employee)formatter.Deserialize(fstream);
                }
            }

            return employee;
        }
    }
}