using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Domain.Repository;

namespace Data.Repository;

public class OracleDBConnection : IDBConnection
{
    private OracleConnection? connection;
    public bool Connected => connection != null && connection.State == ConnectionState.Open;

    public void Connect(string username, string password, string datasource)
    {
        if (Connected)
            Disconnect();

        var connString = $"User Id={username};Password={password};Data Source={datasource};";
        connection = new OracleConnection(connString);
        connection.Open();
    }

    public void Disconnect()
    {
        if (connection != null)
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }
    }
    
    public DataTable ExecuteQuery(string sql, params (string, object)[] parameters)
    {
        if (!Connected)
            throw new InvalidOperationException("Not connected to the database.");

        using var cmd = connection!.CreateCommand();
        cmd.CommandText = sql;

        foreach (var (name, value) in parameters)
        {
            cmd.Parameters.Add(new OracleParameter(name, value ?? DBNull.Value));
        }

        var table = new DataTable();
        using var adapter = new OracleDataAdapter(cmd);
        adapter.Fill(table);

        return table;
    }

    public int ExecuteNonQuery(string sql, params (string, object)[] parameters)
    {
        if (!Connected)
            throw new InvalidOperationException("Not connected to the database.");

        using var cmd = connection!.CreateCommand();
        cmd.CommandText = sql;

        foreach (var (name, value) in parameters)
        {
            cmd.Parameters.Add(new OracleParameter(name, value ?? DBNull.Value));
        }

        return cmd.ExecuteNonQuery();
    }
    
    public void Create(string sysUsername, string sysPassword, string datasource, string schema, string password)
    {
        string sysConnString = $"User Id={sysUsername};Password={sysPassword};Data Source={datasource}";
        using var connection = new OracleConnection(sysConnString);
        connection.Open();

        using var cmd = connection.CreateCommand();

        cmd.CommandText = $"CREATE USER {schema} IDENTIFIED BY {password}";
        cmd.ExecuteNonQuery();

        cmd.CommandText = $"GRANT CONNECT, RESOURCE TO {schema}";
        cmd.ExecuteNonQuery();

        connection.Close();
    }

    public void Delete(string sysUsername, string sysPassword, string datasource, string schema)
    {
        string sysConnString = $"User Id={sysUsername};Password={sysPassword};Data Source={datasource}";
        using var connection = new OracleConnection(sysConnString);
        connection.Open();

        using var cmd = connection.CreateCommand();

        cmd.CommandText = $"DROP USER {schema}";
        cmd.ExecuteNonQuery();

        connection.Close();
    }
}
