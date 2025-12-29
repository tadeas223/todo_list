namespace Domain.Model;

public class Task
{
    private int id;
    private string name;
    private string desc;
    private TaskState state;
    private float progress;
    private DateTime finishDate;

    public int Id => id;
    public string Name => name;
    public string Desc => desc;
    public TaskState State => state;
    public float Progress => progress;
    public DateTime FinishDate => finishDate;

    public Task(TaskBuilder builder)
    {
        this.id = builder.Id;
        this.name = builder.Name;
        this.desc = builder.Desc;
        this.state = builder.State;
        this.progress = builder.Progress;
        this.finishDate = builder.FinishDate;
    }
}