using Domain.Interfaces;
using System.Collections.Generic;
using System.IO;


namespace DataAccess
{
    public abstract class BaseFileStreamRepository<T>: IRepository<T> where T : IEntity
    {
        public DirectoryInfo BaseDataDirectory { get; private set; }
        public DirectoryInfo RootDirectory { get; private set; }
        protected BaseFileStreamRepository(string rootDirPath)
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

        public void Add(T obj)
        {
            obj.Id = obj.Id < 0 ? BaseDataDirectory.GetFiles().Length : obj.Id;
            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{obj.Id}.dat"));

            using (var fstream = file.Open(FileMode.Create, FileAccess.Write))
            {
                Save(fstream, obj);
            }
        }

        protected abstract void Save(FileStream fs, T obj);

        public void Remove(T obj)
        {
            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{obj.Id}.dat"));

            if (file.Exists)
            {
                file.Delete();
            }
        }
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

        protected T Get(FileInfo file)
        {
            T entity = default(T);

            if (file.Exists)
            {
                using (var fstream = file.OpenRead())
                {
                    entity = GetEntity(fstream);
                }
            }

            return entity;
        }

        protected abstract T GetEntity(FileStream fs);
    }
}
