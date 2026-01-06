namespace Data.Repository;

using System.Data;
using DI;
using Domain;
using Domain.Model;

public class OracleDBKanbanReportGen : IKanbanReportGen
{
    public List<KanbanReport> Generate()
    {
        List<KanbanReport> list = new();

        var connection = Provider.Instance.ProvideDBConnection();

        string sql = "SELECT project_name, board_name, todo_count, doing_count, done_count, backlog_count FROM v_project_kanban_stat";
        DataTable data = connection.ExecuteQuery(sql);

        foreach (DataRow row in data.Rows)
        {
            string? projectName = row.Field<string>("project_name");
            string? boardName = row.Field<string>("board_name");

            // Convert counts safely
            int todoCount = row["todo_count"] != DBNull.Value ? Convert.ToInt32(row["todo_count"]) : 0;
            int doingCount = row["doing_count"] != DBNull.Value ? Convert.ToInt32(row["doing_count"]) : 0;
            int doneCount = row["done_count"] != DBNull.Value ? Convert.ToInt32(row["done_count"]) : 0;
            int backlogCount = row["backlog_count"] != DBNull.Value ? Convert.ToInt32(row["backlog_count"]) : 0;

            // Skip if project or board name is null
            if (projectName == null || boardName == null) continue;

            list.Add(new KanbanReport(
                projectName,
                boardName,
                todoCount,
                doingCount,
                doneCount,
                backlogCount
            ));
    }

    return list;
    }
}