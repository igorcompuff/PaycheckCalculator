using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T obj);
        void Remove(T obj);
        IEnumerable<T> GetAll();

        T GetById(int id);
    }
}