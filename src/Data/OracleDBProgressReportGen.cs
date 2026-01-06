namespace Data;

using System.Data;
using DI;
using Domain;
using Domain.Model;

public class OracleDBProgressReportGen : IProgressReportGen
{
    public List<TaskProgressReport> Generate()
    {
        List<TaskProgressReport> list = new();

        var connection = Provider.Instance.ProvideDBConnection();

        string sql= "SELECT project_name, board_name, task_count, avg_progress FROM v_project_board_progress";
        DataTable data = connection.ExecuteQuery(sql);
       
        foreach (DataRow row in data.Rows)
        {
            string? projectName = row.Field<string>("project_name");
            string? boardName = row.Field<string>("board_name");
            int taskCount = Convert.ToInt32(row["task_count"]);
            decimal? avgProgress = row.Field<decimal?>("avg_progress");

            if(!avgProgress.HasValue || projectName == null || boardName == null) continue;

            list.Add(new TaskProgressReport(
                projectName,
                boardName,
                taskCount,
                (double)avgProgress!.Value
            ));

        } 

        return list;
    }
}