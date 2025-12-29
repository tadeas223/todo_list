namespace Data.Repository;

using System.Data;
using Domain.Model;
using Domain.Repository;

public class OracleDBProjectRepository: IProjectRepository
{
    private OracleDBConnection connection;
    OracleDBProjectRepository(OracleDBConnection connection)
    {
        this.connection = connection;
    }

    public void Insert(Project project)
    {
        string selectSql = "SELECT id FROM project WHERE id = :id";
        DataTable selectData = connection.ExecuteQuery(selectSql, ("id", project.Id));
        if(selectData.Rows.Count != 0)
        {
            Update(project);
            return;
        }

        string sql = "INSERT INTO project (id, name, locked) VALUES (:id, :name, :locked)";
        connection.ExecuteNonQuery(sql, ("id", project.Id), ("name", project.Name));
    }

    public void Update(Project project)
    {
        string sql = "UPDATE project SET name = :name WHERE id = :id";
        connection.ExecuteNonQuery(sql, ("id", project.Id), ("name", project.Name));
    }

    public void Delete(Project project)
    {

        string sql = "DELETE FROM project WHERE id = :id";
        connection.ExecuteNonQuery(sql, ("id", project.Id));
    }

    public HashSet<Project> SelectAll()
    {
        string sql = "SELECT id, name, locked FROM project";
        DataTable data = connection.ExecuteQuery(sql);

        HashSet<Project> projects = new HashSet<Project>();
        foreach(DataRow row in data.Rows)
        {
            int id = row.Field<int>("id");
            string? name = row.Field<string>("name");
            bool locked = row.Field<bool>("locked");

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
        DataTable data = connection.ExecuteQuery(sql, ("name", name));

        if(data.Rows.Count == 0)
        {
            return null;
        }

        int id = data.Rows[0].Field<int>("id");
        string? resultName = data.Rows[0].Field<string>("name");
        bool locked = data.Rows[0].Field<bool>("locked");

        if(resultName == null) return null;
        return new ProjectBuilder(id)
            .WithName(resultName)
            .WithLocked(locked)
            .Build();
    }

    public Project? SelectById(int id)
    {
        string sql = "SELECT id, name, locked FROM project WHERE id = :id";
        DataTable data = connection.ExecuteQuery(sql, ("id", id));

        if(data.Rows.Count == 0)
        {
            return null;
        }

        int resultId = data.Rows[0].Field<int>("id");
        string? result_name = data.Rows[0].Field<string>("name");
        bool locked = data.Rows[0].Field<bool>("locked");

        if(result_name == null) return null;
        return new ProjectBuilder(resultId)
            .WithName(result_name)
            .WithLocked(locked)
            .Build();
    }
}