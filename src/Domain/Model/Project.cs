namespace Domain.Model;

public class Project
{
    private int id;
    private string name;
    private bool locked;

    public string Name => name;
    public int Id => id;
    public bool Locked => locked;

    public Project(ProjectBuilder builder)
    {
        id = builder.Id;
        name = builder.Name;
        locked = builder.Locked;
    }

}