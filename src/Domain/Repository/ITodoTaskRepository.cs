using Domain.Model;

namespace Domain.Repository;

public interface ITodoTaskRepository
{
    public void Insert(ref TodoTask task);
    public void Update(TodoTask task);
    public void Delete(TodoTask task);
    
    public HashSet<TodoTask> SelectAll();
    public TodoTask? SelectById(int id);

    public HashSet<TodoTask> SelectByBoard(Board board);
}