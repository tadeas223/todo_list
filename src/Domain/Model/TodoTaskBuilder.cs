namespace Domain.Model;

public class TodoTaskBuilder
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

    public TodoTaskBuilder(int id)
    {
        this.id = id;
        name = "";
        desc = "";
        state = TaskState.TODO;
        progress = 0;
        finishDate = DateTime.Now;
    }

    public TodoTaskBuilder(TodoTask original)
    {
        id = original.Id;
        name = original.Name;
        desc = original.Desc;
        state = original.State;
        progress = original.Progress;
        finishDate = original.FinishDate;
    }

    public TodoTaskBuilder WithName(string name)
    {
        Name = name;
        return this;
    }
    
    public TodoTaskBuilder WithDesc(string desc)
    {
        Desc = desc;
        return this;
    }
    
    public TodoTaskBuilder WithState(TaskState state)
    {
        State = state;
        return this;
    }

    public TodoTaskBuilder WithProgress(float progress)
    {
        Progress = progress;
        return this;
    }

    public TodoTaskBuilder WithFinishDate(DateTime finishDate)
    {
        FinishDate = finishDate;
        return this;
    }

    public TodoTask Build()
    {
        return new TodoTask(this);
    }
}