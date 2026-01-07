using System.Data;
using System.Diagnostics.Contracts;

namespace Domain.Repository;

public interface IDBConnection
{
    public bool Connected {get;}
    public void Connect(string username, string password, string datasource);
    public void Disconnect();
    public DataTable ExecuteQuery(string sql, params object[] parameters);
    public int ExecuteNonQuery(string sql, params object[] parameters);

    public void Create(string sysUsername, string sysPassword, string datasource, string username, string password);
    public void Delete(string sysUsername, string sysPassword, string datasource, string username);

    public void BeginTransaction();
    public void Commit();
    public void Rollback();
}