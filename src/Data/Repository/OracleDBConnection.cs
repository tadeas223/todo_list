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
    
    public DataTable ExecuteQuery(string sql, params object[] parameters)
    {
        if (!Connected)
            throw new InvalidOperationException("Not connected to the database.");

        using var cmd = connection!.CreateCommand();
        cmd.CommandText = sql;

        foreach (var value in parameters)
        {
            cmd.Parameters.Add(value);
        }

        var table = new DataTable();
        using var adapter = new OracleDataAdapter(cmd);
        adapter.Fill(table);

        return table;
    }

    public int ExecuteNonQuery(string sql, params object[] parameters)
    {
        if (!Connected)
            throw new InvalidOperationException("Not connected to the database.");

        using var cmd = connection!.CreateCommand();
        cmd.CommandText = sql;

        foreach (var value in parameters)
        {
            cmd.Parameters.Add(value);
        }

        return cmd.ExecuteNonQuery();
    }
    
    public void Create(string sysUsername, string sysPassword, string datasource, string schema, string password)
    {
        string sysConnString = $"User Id={sysUsername};Password={sysPassword};Data Source={datasource}";
        using var connection = new OracleConnection(sysConnString);
        connection.Open();

        schema = schema.ToUpper();

        using var cmd = connection.CreateCommand();

        cmd.CommandText = $"CREATE USER {schema} IDENTIFIED BY {password}";
        cmd.ExecuteNonQuery();

        cmd.CommandText = $"GRANT ALL PRIVILEGES TO {schema}";
        cmd.ExecuteNonQuery();

        connection.Close();

        Connect(schema, password, datasource);

        ExecuteNonQuery("""
            CREATE TABLE project (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                name VARCHAR2(50) NOT NULL,
                locked NUMBER(1) DEFAULT 0 CHECK (locked IN (0, 1))
           )
        """);

        ExecuteNonQuery("""
            CREATE TABLE board (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                name VARCHAR2(50) NOT NULL,
                project_id NUMBER NOT NULL,
                CONSTRAINT fk_board_project FOREIGN KEY (project_id)
                    REFERENCES project(id)
            )
            """);

            ExecuteNonQuery("""
            CREATE TABLE task (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                name VARCHAR2(50) NOT NULL,
                task_desc VARCHAR2(512),
                state VARCHAR2(10) NOT NULL
                    CHECK (state IN ('todo','doing','done','backlog')),
                progress NUMBER(2,1) DEFAULT 0.0 CHECK (progress >= 0 AND progress <= 5),
                finish_date DATE,
                board_id NUMBER NOT NULL,
                CONSTRAINT fk_task_board FOREIGN KEY (board_id)
                    REFERENCES board(id)
            )
            """);

            ExecuteNonQuery("""
            CREATE TABLE calendar (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                name VARCHAR2(50) NOT NULL
            )
            """);
            
            ExecuteNonQuery("""
            CREATE TABLE calendar_task (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                calendar_id NUMBER NOT NULL,
                task_id NUMBER NOT NULL,
                CONSTRAINT fk_calendar_task_calendar FOREIGN KEY (calendar_id)
                    REFERENCES calendar(id),
                CONSTRAINT fk_calendar_task_task FOREIGN KEY (task_id)
                    REFERENCES task(id)
            )
            """);

        connection.Close();
    }

    public void Delete(string sysUsername, string sysPassword, string datasource, string schema)
    {
        string sysConnString = $"User Id={sysUsername};Password={sysPassword};Data Source={datasource}";
        using var connection = new OracleConnection(sysConnString);
        connection.Open();
        
        schema = schema.ToUpper();

        using var cmd = connection.CreateCommand();

        cmd.CommandText = $"DROP USER \"{schema}\" CASCADE";

        cmd.ExecuteNonQuery();

        connection.Close();
    }
}
