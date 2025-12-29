namespace Domain.Model;

public class ProjectBuilder
{
    private int id;
    private string name;

    private bool locked;

    public int Id => id;
    public string Name
    {
        get { return name; }
        set { name = value; }
    } 

    public bool Locked
    {
        get { return locked; }
        set { locked= value; }
    } 

    public ProjectBuilder(int id)
    {
        this.id = id;
        name = "";
        locked = false;
    }

    public ProjectBuilder(Project original)
    {
        id = original.Id;
        name = original.Name;
        locked = original.Locked;
    }

    public ProjectBuilder WithName(string name)
    {
        Name = name;
        return this;
    }
    
    public ProjectBuilder WithLocked(bool locked)
    {
        Locked = locked;
        return this;
    }

    public Project Build()
    {
        return new Project(this);
    }

}