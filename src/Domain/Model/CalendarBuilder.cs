namespace Domain.Model;

public class CalendarBuilder
{
    private int id;
    private string name;

    public int Id => id;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public CalendarBuilder(int id)
    {
        this.id = id;
        name = "";
    }

    public CalendarBuilder(Calendar original)
    {
        id = original.Id;
        name = original.Name;
    }

    public CalendarBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public Calendar Build()
    {
        return new Calendar(this);
    }
}