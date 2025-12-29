using Domain.Model;

namespace Domain.Repository;

public interface ICalendarRepository
{
    public void Insert(ref Calendar calendar);
    public void InsertTask(Calendar calendar, TodoTask task);
    public void Update(Calendar calendar);
    public void Delete(Calendar calendar);

    public HashSet<Calendar> SelectAll();
    public Calendar? SelectByName(string name);
    public Calendar? SelectById(int id);
    public HashSet<TodoTask> SelectCalendarTasks(Calendar calendar);
}