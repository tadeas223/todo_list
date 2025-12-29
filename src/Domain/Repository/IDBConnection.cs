using System.Data;
using System.Diagnostics.Contracts;

namespace Domain.Repository;

public interface IDBConnection
{
    public bool Connected {get;}
    public void Connect(string username, string password, string db);
    public void Disconnect();
    public DataTable ExecuteQuery(string sql, params(string, object)[] parameters);
    public int ExecuteNonQuery(string sql, params(string, object)[] parameters);
}