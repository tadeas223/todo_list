namespace Domain.Model;

public class CalendarBuilder
{
    private int? id;
    private string? name;
    private Project? project;

    public int? Id
    {
        get { return id; }
        set { id = value; }
    }

    public string? Name
    {
        get { return name; }
        set { name = value; }
    }
    public Project? Project
    {
        get { return project; }
        set { project = value; }
    }

    public CalendarBuilder() {}
    public CalendarBuilder(int id)
    {
        this.id = (int?)id;
    }

    public CalendarBuilder(Calendar original)
    {
        id = original.Id;
        name = original.Name;
        project = original.Project;
    }

    public CalendarBuilder WithName(string name)
    {
        Name = name;
        return this;
    }
    
    public CalendarBuilder WithProject(Project project)
    {
        Project = project;
        return this;
    }
    
    public CalendarBuilder WithId(int id)
    {
        Id = id;
        return this;
    }



    public Calendar Build()
    {
        if(project == null || name == null)
        {
            throw new Exception("not all params set");
        }
        return new Calendar(this);
    }
}