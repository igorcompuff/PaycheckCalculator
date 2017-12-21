using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Domain;

namespace DataAccess
{
    public class SerializationFileStreamEmployeeRepository : BaseFileStreamEmployeeRepository
    {
        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        public SerializationFileStreamEmployeeRepository(string rootDirPath): base(rootDirPath)
        {
        }

        public override void Add(Employee obj)
        {
            obj.Id = obj.Id < 0 ? BaseDataDirectory.GetFiles().Length : obj.Id;
            var file = new FileInfo(Path.Combine(BaseDataDirectory.FullName, $"{obj.Id}.dat"));

            using (var fstream = file.Open(FileMode.Create, FileAccess.Write))
            {
                _formatter.Serialize(fstream, obj);
            }
        }

        protected override Employee GetEmployee(FileInfo file)
        {
            Employee employee = null;

            if (file.Exists)
            {
                using (var fstream = file.OpenRead())
                {
                    employee = (Employee)_formatter.Deserialize(fstream);
                }
            }

            return employee;
        }
    }
}