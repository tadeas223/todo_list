namespace Domain.Model;

public class KanbanReport
{
    public string ProjectName {get; set;}
    public string BoardName {get; set;}
    public int TodoCount {get; set;}
    public int DoingCount {get; set;}
    public int DoneCount {get; set;}
    public int BacklogCount {get; set;}

    public KanbanReport(string projectName,
        string boardName,
        int todoCount,
        int doingCount,
        int doneCount,
        int backlogCount
    )
    {
        ProjectName = projectName;
        BoardName = boardName;
        TodoCount = todoCount;
        DoneCount = doneCount;
        DoingCount = doingCount;
        BacklogCount = backlogCount;
    }
}
