namespace Domain.Model;

public class Calendar
{
    private int? id;
    private string name;
    private Project project;
    
    public int? Id => id;
    public string Name => name;

    public Project Project => project;

    public Calendar(CalendarBuilder builder)
    {
        id = builder.Id;
        name = builder.Name!;
        project = builder.Project!;
    }
}
