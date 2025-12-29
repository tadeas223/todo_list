namespace Domain.Model;

class Calendar
{
    private int id;
    private string name;
    
    public int Id => id;
    public string Name => name;

    public Calendar(CalendarBuilder builder)
    {
        this.id = builder.Id;
        this.name = builder.Name;
    }
}