namespace Domain.Repository;

public interface ITaskRepository
{
    public void insert(Task task);
    public void update(Task task);
    public void delete(Task task);
    public void se(Task task);
}