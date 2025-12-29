namespace Domain.Model;

public class TodoTask
{
    private int? id;
    private string name;
    private string desc;
    private TaskState state;
    private float progress;
    private DateTime finishDate;
    private Board board;

    public int? Id => id;
    public string Name => name;
    public string Desc => desc;
    public TaskState State => state;
    public float Progress => progress;
    public DateTime FinishDate => finishDate;
    public Board Board => board;

    public TodoTask(TodoTaskBuilder builder)
    {
        this.id = builder.Id;
        this.name = builder.Name!;
        this.desc = builder.Desc!;
        this.state = builder.State!.Value;
        this.progress = builder.Progress!.Value;
        this.finishDate = builder.FinishDate!.Value;
        this.board = builder.Board!;
    }
}