namespace Domain.Model;

public class TaskProgressReport
{
    public string ProjectName { get; set; }
    public string BoardName { get; set; }
    public int TaskCount { get; set; }
    public double ProgressAvg { get; set; }

    public TaskProgressReport(string projectName, string boardName, int taskCount, double progressAvg)
    {
        ProjectName = projectName;
        BoardName = boardName;
        TaskCount = taskCount;
        ProgressAvg = progressAvg;
    }

    public TaskProgressReport() { 
        ProjectName = ""; 
        BoardName = "";
    }
}
