using System.Data;
using DI;
using Domain.Model;
using Domain.Repository;
using Oracle.ManagedDataAccess.Client;

namespace Data.Repository;

public class OracleDBCalendarRepository: ICalendarRepository
{
    private IDBConnection connection;
    
    public OracleDBCalendarRepository(IDBConnection connection)
    {
        this.connection = connection;
    } 

    public void Insert(ref Calendar calendar)
    {
        var outId =  new OracleParameter("newId", OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output
        };

        string sql = "INSERT INTO calendar (name, project_id) VALUES (:name, :project_id) RETURNING Id INTO :newId";
        connection.ExecuteNonQuery(sql, 
            ("name", calendar.Name), 
            ("project_id", calendar.Project.Id), 
            ("newId", outId)
        );

        int newId = Convert.ToInt32(outId.Value);

        Calendar newCalendar = new CalendarBuilder(newId)
        .WithName(calendar.Name)
        .WithProject(calendar.Project)
        .Build();

        calendar = newCalendar;
    }

    public void InsertTask(Calendar calendar, TodoTask task)
    {
        var outId = new OracleParameter("newId", OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output
        };

        string sql = "INSERT INTO calendar_task (calendar_id, task_id) VALUES (:calendar_id, :task_id) RETURNING Id INTO :newId";
        connection.ExecuteNonQuery(sql,
            ("calendar_id", calendar.Id),
            ("task_id", task.Id),
            ("newId", outId)
        );
    }

    public void Update(Calendar calendar)
    {
        string sql = "UPDATE calendar SET name = :name WHERE id = :id";
        connection.ExecuteNonQuery(sql,
            ("name", calendar.Name),
            ("id", calendar.Id)
        );
    }

    public void Delete(Calendar calendar)
    {
        string sqlTasks = "DELETE FROM calendar_task WHERE calendar_id = :calendar_id";
        connection.ExecuteNonQuery(sqlTasks, ("calendar_id", calendar.Id));

        string sql = "DELETE FROM calendar WHERE id = :id";
        connection.ExecuteNonQuery(sql, ("id", calendar.Id));
    }

    public HashSet<Calendar> SelectAll()
    {
        string sql = "SELECT id, name, project_id FROM calendar";
        DataTable dt = connection.ExecuteQuery(sql);
        var result = new HashSet<Calendar>();
        foreach (DataRow row in dt.Rows)
        {
            string? name = row.Field<string>("name");
            if(name == null) continue;

            var calendar = new CalendarBuilder(Convert.ToInt32(row["id"]))
                .WithName(name)
                .WithProject(new ProjectBuilder(Convert.ToInt32(row["project_id"])).Build())
                .Build();
            result.Add(calendar);
        }
        return result;
    }

    public Calendar? SelectByName(string name)
    {
        string sql = "SELECT id, name, project_id FROM calendar WHERE name = :name";
        DataTable dt = connection.ExecuteQuery(sql, ("name", name));
        if (dt.Rows.Count == 0) return null;
        DataRow row = dt.Rows[0];
        string? newName = row.Field<string>("name");
        
        if(newName == null) return null;
        
        return new CalendarBuilder(Convert.ToInt32(row["id"]))
            .WithName(newName)
            .WithProject(new ProjectBuilder(Convert.ToInt32(row["project_id"])).Build())
            .Build();
    }

    public Calendar? SelectById(int id)
    {
        string sql = "SELECT id, name, project_id FROM calendar WHERE id = :id";
        DataTable dt = connection.ExecuteQuery(sql, ("id", id));
        if (dt.Rows.Count == 0) return null;
        DataRow row = dt.Rows[0];

        string? name = row.Field<string>("name");
        if(name == null) return null;

        return new CalendarBuilder(Convert.ToInt32(row["id"]))
            .WithName(name)
            .WithProject(new ProjectBuilder(Convert.ToInt32(row["project_id"])).Build())
            .Build();
    }

    public HashSet<TodoTask> SelectCalendarTasks(Calendar calendar)
    {
        string sql = """
            SELECT t.id
            FROM task t
            JOIN calendar_task ct ON t.id = ct.task_id
            WHERE ct.calendar_id = :calendar_id;
        """;

        DataTable dt = connection.ExecuteQuery(sql, ("calendar_id", calendar.Id));
        var tasks = new HashSet<TodoTask>();

        foreach (DataRow row in dt.Rows)
        {
            int taskId = row.Field<int>("id");
            TodoTask? task = Provider.Instance.ProvideTodoTaskRepository().SelectById(taskId);
            if(task == null) continue;

            tasks.Add(task);
        }

        return tasks;
    }
}
