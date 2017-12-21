using Domain;
using Domain.Interfaces;
using System.Collections.Generic;
using System.IO;


namespace DataAccess
{
    public abstract class BaseFileStreamEmployeeRepository: IRepository<Employee>
    {
        public DirectoryInfo BaseDataDirectory { get; private set; }
        public DirectoryInfo RootDirectory { get; private set; }

        protected BaseFileStreamEmployeeRepository(string rootDirPath)
        {
            CreateRootDirectory(rootDirPath);
            CreateDataBaseDirectory();
        }

        private void CreateRootDirectory(string baseDirPath)
        {
            RootDirectory = new DirectoryInfo(baseDirPath);

            if (!RootDirectory.Exists)
            {
                RootDirectory.Create();
            }
        }
        private void CreateDataBaseDirectory()
        {
            BaseDataDirectory = RootDirectory.CreateSubdirectory("Employees");
        }

        public abstract void Add(Employee obj);

        public IEnumerable<Employee> GetAll()
        {
            var employees = new List<Employee>();

            foreach (var employeeFile in BaseDataDirectory.GetFiles())
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
            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{id}.dat"));
            return GetEmployee(file);
        }

        public void Remove(Employee obj)
        {
            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{obj.Id}.dat"));

            if (file.Exists)
            {
                file.Delete();
            }
        }

        protected abstract Employee GetEmployee(FileInfo file);
    }
}
