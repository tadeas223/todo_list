using System.Globalization;

namespace Domain.Repository;

public interface ICalendarRepository
{
    public void Insert(Calendar calendar);
    public void Update(Calendar calendar);
    public void Delete(Calendar calendar);

    public HashSet<Calendar> SelectAll();
    public Calendar? SelectByName(string name);
    public Calendar? SelectById(int id);
    public Calendar? SelectCalendarTasks(string name);
    public Calendar? SelectCalendarTasksByDate(Calendar calendar, DateTime date);
}