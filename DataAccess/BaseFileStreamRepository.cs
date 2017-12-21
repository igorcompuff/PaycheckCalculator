using Domain.Interfaces;
using System.Collections.Generic;
using System.IO;


namespace DataAccess
{
    public abstract class BaseFileStreamEmployeeRepository<T>: IRepository<T>
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
            BaseDataDirectory = RootDirectory.CreateSubdirectory(typeof(T).Name);
        }

        public abstract void Add(T obj);
        public abstract void Remove(T obj);

        public virtual IEnumerable<T> GetAll()
        {
            var entities = new List<T>();

            foreach (var employeeFile in BaseDataDirectory.GetFiles())
            {
                var employee = Get(employeeFile);

                if (employee != null)
                {
                    entities.Add(employee);
                }
            }
            return entities;
        }

        public virtual T GetById(int id)
        {
            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{id}.dat"));
            return Get(file);
        }

        protected abstract T Get(FileInfo file);
    }
}
