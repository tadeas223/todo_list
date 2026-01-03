using System.Data;
using Domain.Model;
using Domain.Repository;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Data.Repository;

class OracleDBBoardRepository: IBoardRepository
{
    private IDBConnection connection;
    public OracleDBBoardRepository(IDBConnection connection)
    {
        this.connection = connection;
    }

    public void Insert(ref Board board)
    {
        var outId = new OracleParameter("newId", OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output
        };

        if(board.Project.Id == null)
        {
            throw new ArgumentNullException("board project does not have an id");
        }

        string sql = "INSERT INTO board (name, project_id) VALUES (:name, :project_id) RETURNING Id INTO :newId";
        connection.ExecuteNonQuery(sql, 
            new OracleParameter("name", OracleDbType.Varchar2) { Value = board.Name }, 
            new OracleParameter("project_id", OracleDbType.Int32) { Value = board.Project.Id }, 
            outId
        );

        int newId = ((OracleDecimal)outId.Value).ToInt32();

        board = new BoardBuilder(newId)
            .WithName(board.Name)
            .WithProject(board.Project)
            .Build();
    }

    public void Update(Board board)
    {
        string sql = "UPDATE board SET name = :name, project_id = :project_id WHERE id = :id";
        
        if(board.Project.Id == null)
        {
            throw new ArgumentNullException("board project does not have an id");
        }
        
        if(board.Id == null)
        {
            throw new ArgumentNullException("board does not have an id");
        }

        connection.ExecuteNonQuery(sql,
            new OracleParameter("name", OracleDbType.Varchar2) { Value = board.Name },
            new OracleParameter("project_id", OracleDbType.Int32) { Value = board.Project.Id },
            new OracleParameter("id", OracleDbType.Int32) { Value = board.Id }
        );
    }

    public void Delete(Board board)
    {
        string sql = "DELETE FROM board WHERE id = :id";
        if(board.Id == null)
        {
            throw new ArgumentNullException("board does not have an id");
        }
        connection.ExecuteNonQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = board.Id }
        );
    }

    public HashSet<Board> SelectAll()
    {
        string sql = "SELECT id, name, project_id FROM board";
        DataTable dt = connection.ExecuteQuery(sql);
        var result = new HashSet<Board>();

        foreach (DataRow row in dt.Rows)
        {
            string? name = row.Field<string>("name");
            if(name == null) continue;
            
            var board = new BoardBuilder(Convert.ToInt32(row["id"]))
                .WithName(name)
                .WithProject(new ProjectBuilder(Convert.ToInt32(row["project_id"])).Build())
                .Build();
            result.Add(board);
        }

        return result;
    }

    public Board? SelectById(int id)
    {
        string sql = "SELECT id, name, project_id FROM board WHERE id = :id";
        DataTable dt = connection.ExecuteQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = id }
        );
        if (dt.Rows.Count == 0) return null;

        DataRow row = dt.Rows[0];
        string? name = row.Field<string>("name");
        if(name == null) return null;

        return new BoardBuilder(Convert.ToInt32(row["id"]))
            .WithName(name)
            .WithProject(new ProjectBuilder(Convert.ToInt32(row["project_id"])).Build())
            .Build();
    }
}
