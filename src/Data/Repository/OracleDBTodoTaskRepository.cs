namespace Data.Repository;

using Domain.Repository;
using Domain.Model;
using DI;
using System.ComponentModel;
using System.Data.Common;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

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
            INSERT INTO task (name, task_desc, state, progress, finish_date, board_id) 
            VALUES (:name, :desc, :state, :progress, :finish_date, :board_id)
             RETURNING Id INTO :newId;
        """;

        if(task.Board.Id == null)
        {
            throw new ArgumentNullException("task board does not have an id");
        }

        connection.ExecuteNonQuery(sql, 
            new OracleParameter("name", OracleDbType.Varchar2) { Value = task.Name }, 
            new OracleParameter("task_desc", OracleDbType.Varchar2) { Value = task.Desc },
            new OracleParameter("state", OracleDbType.Varchar2) { Value = task.State.ToString().ToLower() },
            new OracleParameter("progress", OracleDbType.Decimal) { Value = task.Progress },
            new OracleParameter("finish_date", OracleDbType.Date) { Value = task.FinishDate },
            new OracleParameter("board_id", OracleDbType.Int32) { Value = task.Board.Id },
            outId
        );

        int newId = ((OracleDecimal)outId.Value).ToInt32();

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
                task_desc = :desc
                state = :state
                progress = :progress
                finish_date = :finish_date
                board_id = :board_id
            WHERE id = :id
        """;

        if(task.Board.Id == null)
        {
            throw new ArgumentNullException("task board does not have an id");
        }

        connection.ExecuteNonQuery(sql, 
            new OracleParameter("name", OracleDbType.Varchar2) { Value = task.Name }, 
            new OracleParameter("task_desc", OracleDbType.Varchar2) { Value = task.Desc },
            new OracleParameter("state", OracleDbType.Varchar2) { Value = task.State.ToString().ToLower() },
            new OracleParameter("progress", OracleDbType.Decimal) { Value = task.Progress },
            new OracleParameter("finish_date", OracleDbType.Date) { Value = task.FinishDate },
            new OracleParameter("board_id", OracleDbType.Int32) { Value = task.Board.Id }
        );
    }

    public void Delete(TodoTask task)
    {
        string sql = "DELETE FROM task WHERE id = :id";

        if(task.Id == null)
        {
            throw new ArgumentNullException("task does not have an id");
        }

        connection.ExecuteNonQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = task.Id }
        );
    }
    
    public HashSet<TodoTask> SelectAll()
    {
        string sql = "SELECT * FROM task";
        DataTable data = connection.ExecuteQuery(sql);

        HashSet<TodoTask> tasks = new HashSet<TodoTask>();
        foreach(DataRow row in data.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            string? name = row.Field<string>("name");
            string? desc = row.Field<string>("task_desc");
            string? state = row.Field<string>("state");
            float progress = (float)Convert.ToDouble(row["progress"]);
            DateTime finish_date = Convert.ToDateTime(row["finish_date"]);

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
        DataTable data = connection.ExecuteQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = id }
        );

        if(data.Rows.Count == 0)
        {
            return null;
        }

        var row = data.Rows[0];

            int newId = Convert.ToInt32(row["id"]);
            string? name = row.Field<string>("name");
            string? desc = row.Field<string>("task_desc");
            string? state = row.Field<string>("state");
            float progress = (float)Convert.ToDouble(row["progress"]);
            DateTime finish_date = Convert.ToDateTime(row["finish_date"]);

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
        
        var board = boardRepository.SelectById(newId);
        if(board == null) return null; 
        
        return new TodoTaskBuilder(newId)
            .WithBoard(board)
            .WithName(name)
            .WithDesc(desc)
            .WithState(stateEnum)
            .WithProgress(progress)
            .WithFinishDate(finish_date)
            .Build();
    }
    
    public HashSet<TodoTask> SelectByBoard(Board board)
    {
        string sql = "SELECT * FROM task WHERE board_id = :id";
        DataTable data = connection.ExecuteQuery(sql,
            new OracleParameter("id", OracleDbType.Int32) { Value = board.Id }
        );

        HashSet<TodoTask> tasks = new HashSet<TodoTask>();
        foreach(DataRow row in data.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            string? name = row.Field<string>("name");
            string? desc = row.Field<string>("task_desc");
            string? state = row.Field<string>("state");
            float progress = (float)Convert.ToDouble(row["progress"]);
            DateTime finish_date = Convert.ToDateTime(row["finish_date"]);

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
}