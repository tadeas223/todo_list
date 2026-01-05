namespace Data.Repository;

using System.Data;
using Domain.Model;
using Domain.Repository;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

public class OracleDBProjectRepository: IProjectRepository
{
    private IDBConnection connection;
    public OracleDBProjectRepository(IDBConnection connection)
    {
        this.connection = connection;
    }

    public void Insert(ref Project project)
    {
        var outId =  new OracleParameter("newId", OracleDbType.Int32)
        {
            Direction = ParameterDirection.Output
        };

        string sql = "INSERT INTO project (name, locked) VALUES (:name, :locked) RETURNING Id INTO :newId";
        connection.ExecuteNonQuery(sql, 
            new OracleParameter("name", OracleDbType.Varchar2) { Value = project.Name }, 
            new OracleParameter("locked",  OracleDbType.Int32) { Value = project.Locked? 1: 0},
            outId
        );

        int newId = ((OracleDecimal)outId.Value).ToInt32();

        Project newProject = new ProjectBuilder(newId)
            .WithName(project.Name)
            .WithLocked(project.Locked)
            .Build();

        project = newProject;
    }

    public void Update(Project project)
    {
        string sql = "UPDATE project SET name = :name, locked = :locked WHERE id = :id";

        if(project.Id == null)
        {
            throw new ArgumentNullException("project does not have an id");
        }
        connection.ExecuteNonQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = project.Id }, 
            new OracleParameter("name", OracleDbType.Varchar2) { Value = project.Name },
            new OracleParameter("locked", OracleDbType.Int32) { Value = project.Locked? 1 : 0 }
        );
    }

    public void Delete(Project project)
    {
        string sql = "DELETE FROM project WHERE id = :id";
        if(project.Id == null)
        {
            throw new ArgumentNullException("project does not have an id");
        }
        connection.ExecuteNonQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = project.Id! }
        );
    }

    public HashSet<Project> SelectAll()
    {
        string sql = "SELECT id, name, locked FROM project";
        DataTable data = connection.ExecuteQuery(sql);

        HashSet<Project> projects = new HashSet<Project>();
        foreach(DataRow row in data.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            string? name = row.Field<string>("name");
            bool locked = Convert.ToBoolean(row["locked"]);

            if(name == null) continue;

            Project project = new ProjectBuilder(id)
                .WithName(name)
                .WithLocked(locked)
                .Build();

            projects.Add(project);
        }

        return projects;
    }

    public Project? SelectByName(string name)
    {
        string sql = "SELECT id, name, locked FROM project WHERE name = :name";
        DataTable data = connection.ExecuteQuery(sql, 
            new OracleParameter("name", OracleDbType.Varchar2) { Value = name }
        );

        if(data.Rows.Count == 0)
        {
            return null;
        }

        int id = Convert.ToInt32(data.Rows[0]["id"]);
        string? resultName = data.Rows[0].Field<string>("name");
        bool locked = Convert.ToBoolean(data.Rows[0]["locked"]);

        if(resultName == null) return null;
        return new ProjectBuilder(id)
            .WithName(resultName)
            .WithLocked(locked)
            .Build();
    }

    public Project? SelectById(int id)
    {
        string sql = "SELECT id, name, locked FROM project WHERE id = :id";
        DataTable data = connection.ExecuteQuery(sql, 
            new OracleParameter("id", OracleDbType.Int32) { Value = id }
        );

        if(data.Rows.Count == 0)
        {
            return null;
        }

        int resultId = Convert.ToInt32(data.Rows[0]["id"]);
        string? result_name = data.Rows[0].Field<string>("name");
        bool locked = Convert.ToBoolean(data.Rows[0]["locked"]);

        if(result_name == null) return null;
        return new ProjectBuilder(resultId)
            .WithName(result_name)
            .WithLocked(locked)
            .Build();
    }
}