using System.Collections.Generic;

namespace projectC.Repository;

public interface GenericInterface<T, ID>
{
    void Add(T t);
    void Remove(ID id);
    void Update(ID id, T t);
    List<T> GetAll();
}