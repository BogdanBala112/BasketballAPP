package Repository;

import java.util.List;

public interface GenericInterface<T, ID>{
    void add(T t);
    void remove(ID id);
    T getById(ID id);
    void update(ID id, T t);
    List<T> getAll();
}
