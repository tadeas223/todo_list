using System.ComponentModel;

namespace Domain.Model;

public class ProjectBuilder
{
    private int? id;
    private string? name;

    private bool? locked;

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

    public bool? Locked
    {
        get { return locked; }
        set { locked= value; }
    } 

    public ProjectBuilder(int id)
    {
        this.id = id;
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
    
    public ProjectBuilder WithId(int id)
    {
        Id = id;
        return this;
    }

    public Project Build()
    {
        if(name == null || locked == null)
        {
            throw new Exception("name must be set");
        }
        return new Project(this);
    }

}