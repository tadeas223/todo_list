namespace Domain.Model;

public class BoardBuilder
{
    private int? id;
    private string? name;
    private Project? project;

    public int? Id
    {
        get { return id; }
        set {id = value; }
    } 

    public string? Name
    {
        get { return name; }
        set {name = value; }
    } 
    
    public Project? Project
    {
        get { return project; }
        set {project = value; }
    } 

    public BoardBuilder(int id)
    {
        this.id = id;
    }

    public BoardBuilder(Board original)
    {
        id = original.Id;
        name = original.Name;
        project = original.Project;
    }

    public BoardBuilder WithName(string name)
    {
        Name = name;
        return this;
    }
    
    public BoardBuilder WithProject(Project project)
    {
        Project = project;
        return this;
    }
    
    public BoardBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public Board Build()
    {
        if(name == null || project == null)
        {
            throw new Exception("not all params set");
        }
        return new Board(this);
    }
}