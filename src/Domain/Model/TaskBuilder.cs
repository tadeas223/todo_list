namespace Domain.Model;

public class TaskBuilder
{
    private int id;
    private string name;
    private string desc;
    private TaskState state;
    private float progress;
    private DateTime finishDate;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public string Desc
    {
        get { return desc; }
        set { desc = value; }
    }
    public TaskState State
    {
        get { return state; }
        set { state = value; }
    }
    public float Progress
    {
        get { return progress; }
        set { progress = value; }
    }
    public DateTime FinishDate
    {
        get { return finishDate; }
        set { finishDate = value; }
    }

    public TaskBuilder(int id)
    {
        this.id = id;
        name = "";
        desc = "";
        state = TaskState.TODO;
        progress = 0;
        finishDate = DateTime.Now;
    }

    public TaskBuilder(Task original)
    {
        id = original.Id;
        name = original.Name;
        desc = original.Desc;
        state = original.State;
        progress = original.Progress;
        finishDate = original.FinishDate;
    }

    public TaskBuilder WithName(string name)
    {
        Name = name;
        return this;
    }
    
    public TaskBuilder WithDesc(string desc)
    {
        Desc = desc;
        return this;
    }
    
    public TaskBuilder WithState(TaskState state)
    {
        State = state;
        return this;
    }

    public TaskBuilder WithProgress(float progress)
    {
        Progress = progress;
        return this;
    }

    public TaskBuilder WithFinishDate(DateTime finishDate)
    {
        FinishDate = finishDate;
        return this;
    }

    public Task Build()
    {
        return new Task(this);
    }
}