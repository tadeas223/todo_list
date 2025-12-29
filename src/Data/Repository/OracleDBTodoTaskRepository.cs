namespace Data.Repository;

using Domain.Repository;
using Domain.Model;
using DI;
using System.ComponentModel;
using System.Data.Common;
using System.Data;
using Oracle.ManagedDataAccess.Client;

class OracleDBTodoTaskRepository: ITodoTaskRepository
{
    private IDBConnection connection;
    private IBoardRepository boardRepository;

    public OracleDBTodoTaskRepository(IDBConnection connection)
    {
        this.connection = connection;
        boardRepository = Provider.Instance.ProvideBoardRepository();
    }

    public void Insert(ref TodoTask task)
    {
        var outId =  new OracleParameter("newId", OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output
        };

        string sql = """
            INSERT INTO task (name, desc, state, progress, finish_date, board_id) 
            VALUES (:name, :desc, :state, :progress, :finish_date, :board_id)
             RETURNING Id INTO :newId;
        """;

        connection.ExecuteNonQuery(sql, 
            ("name", task.Name), 
            ("desc", task.Desc),
            ("state", task.State.ToString().ToLower()),
            ("progress", task.Progress),
            ("finish_date", task.FinishDate),
            ("board_id", task.Board.Id),
            ("newId", outId)
        );

        int newId = Convert.ToInt32(outId.Value);

        TodoTask newTask = new TodoTaskBuilder(newId)
            .WithBoard(task.Board)
            .WithName(task.Name)
            .WithDesc(task.Desc)
            .WithState(task.State)
            .WithProgress(task.Progress)
            .WithFinishDate(task.FinishDate)
            .Build();
        task = newTask;
    }

    public void Update(TodoTask task)
    {
        string sql = """
            UPDATE task SET 
                name = :name 
                desc = :desc
                state = :state
                progress = :progress
                finish_date = :finish_date
                board_id = :board_id
            WHERE id = :id
        """;

        connection.ExecuteNonQuery(sql, 
            ("name", task.Name), 
            ("desc", task.Desc),
            ("state", task.State.ToString().ToLower()),
            ("progress", task.Progress),
            ("finish_date", task.FinishDate),
            ("board_id", task.Board.Id)
        );
    }

    public void Delete(TodoTask task)
    {
        string sql = "DELETE FROM task WHERE id = :id";
        connection.ExecuteNonQuery(sql, ("id", task.Id));
    }
    
    public HashSet<TodoTask> SelectAll()
    {
        string sql = "SELECT * FROM task";
        DataTable data = connection.ExecuteQuery(sql);

        HashSet<TodoTask> tasks = new HashSet<TodoTask>();
        foreach(DataRow row in data.Rows)
        {
            int id = row.Field<int>("id");
            string? name = row.Field<string>("name");
            string? desc = row.Field<string>("desc");
            string? state = row.Field<string>("state");
            float progress = row.Field<float>("progress");
            DateTime finish_date = row.Field<DateTime>("finish_date");

            if(name == null
                || desc == null
                || state == null
            ) continue;

            TaskState stateEnum = TaskState.TODO;

            switch(state)
            {
                case "todo":
                    stateEnum = TaskState.TODO;
                    break;
                case "doing":
                    stateEnum = TaskState.DOING;
                    break;
                case "done":
                    stateEnum = TaskState.DONE;
                    break;
                case "backlog":
                    stateEnum = TaskState.BACKLOG;
                    break;
            }

            var board = boardRepository.SelectById(id);
            if(board == null) continue;

            TodoTask newTask = new TodoTaskBuilder(id)
                .WithBoard(board)
                .WithName(name)
                .WithDesc(desc)
                .WithState(stateEnum)
                .WithProgress(progress)
                .WithFinishDate(finish_date)
                .Build();

            tasks.Add(newTask);
        }

        return tasks;
    }

    public TodoTask? SelectById(int id)
    {
        string sql = "SELECT * FROM task WHERE id = :id";
        DataTable data = connection.ExecuteQuery(sql, ("id", id));

        if(data.Rows.Count == 0)
        {
            return null;
        }

        var row = data.Rows[0];

        int newId = row.Field<int>("id");
        string? name = row.Field<string>("name");
        string? desc = row.Field<string>("desc");
        string? state = row.Field<string>("state");
        float progress = row.Field<float>("progress");
        DateTime finish_date = row.Field<DateTime>("finish_date");

        if(name == null
            || desc == null
            || state == null
        )
        {
            return null;
        }

        TaskState stateEnum = TaskState.TODO;

        switch(state)
        {
            case "todo":
                stateEnum = TaskState.TODO;
                break;
            case "doing":
                stateEnum = TaskState.DOING;
                break;
            case "done":
                stateEnum = TaskState.DONE;
                break;
            case "backlog":
                stateEnum = TaskState.BACKLOG;
                break;
        }
        
        var board = boardRepository.SelectById(id);
        if(board == null) return null; 
        
        return new TodoTaskBuilder(id)
            .WithBoard(board)
            .WithName(name)
            .WithDesc(desc)
            .WithState(stateEnum)
            .WithProgress(progress)
            .WithFinishDate(finish_date)
            .Build();
    }
}