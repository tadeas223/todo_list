namespace Data.Repository;

using Domain.Repository;
using Domain.Model;

class OracleDBTodoTaskRepository: ITodoTaskRepository
{
    private OracleDBConnection connection;

    OracleDBTodoTaskRepository(OracleDBConnection connection)
    {
        this.connection = connection;
    }

    public void Insert(TodoTask task)
    {
        string sql = "INSERT INTO task(id, name, desc, state, progress, finish_data, board_id) "
    }

    public void Update(TodoTask task)
    {

    }
    public void Delete(TodoTask task)
    {

    }
    
    public HashSet<TodoTask> SelectAll()
    {

    }
    public TodoTask? SelectById(int id)
    {

    }
    public TodoTask? SelectByName(string name)
    {

    }

    public HashSet<TodoTask> SelectByState(TaskState state)
    {

    }
}