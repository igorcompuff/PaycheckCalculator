using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Domain.Interfaces;

namespace DataAccess
{
    public class SerializationFileStreamEntityRepository<T> : BaseFileStreamRepository<T> where T:IEntity
    {
        private readonly BinaryFormatter _formatter = new BinaryFormatter();

        public SerializationFileStreamEntityRepository(string rootDirPath): base(rootDirPath)
        {
        }

        protected override void Save(FileStream fs, T obj)
        {
            _formatter.Serialize(fs, obj);
        }

        protected override T GetEntity(FileStream fs)
        {
            return (T)_formatter.Deserialize(fs);
        }
    }
}