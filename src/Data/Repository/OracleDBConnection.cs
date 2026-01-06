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
            OracleConnection.ClearPool(connection);
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
        cmd.BindByName = true;

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

        cmd.BindByName = true;
        return cmd.ExecuteNonQuery();
    }
    
    public void Create(string sysUsername, string sysPassword, string datasource, string schema, string password)
    {
        string sysConnString = $"User Id={sysUsername};Password={sysPassword};Data Source={datasource}/XE";
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
                name VARCHAR2(50) UNIQUE NOT NULL,
                locked NUMBER(1) DEFAULT 0 CHECK (locked IN (0, 1))
            )
        """);

        ExecuteNonQuery("""
            CREATE TABLE board (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                name VARCHAR2(50) UNIQUE NOT NULL,
                project_id NUMBER NOT NULL,
                FOREIGN KEY (project_id)
                    REFERENCES project(id)
                    ON DELETE CASCADE
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
                FOREIGN KEY (board_id)
                    REFERENCES board(id)
                    ON DELETE CASCADE
            )
        """);

        ExecuteNonQuery("""
            CREATE TABLE calendar (
                id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                project_id NUMBER NOT NULL,
                name VARCHAR2(50) NOT NULL,
                FOREIGN KEY (project_id)
                    REFERENCES project(id)
                    ON DELETE CASCADE
            )
        """);
        
        ExecuteNonQuery("""
            CREATE VIEW v_project_board_progress AS
                SELECT
                    p.name AS project_name,
                    b.name AS board_name,
                    COUNT(t.id) AS task_count,
                    ROUND(AVG(t.progress), 1) AS avg_progress
                FROM project p
                JOIN board b ON b.project_id = p.id
                LEFT JOIN task t ON t.board_id = b.id
                GROUP BY p.name, b.name
        """);

        ExecuteNonQuery("""
            CREATE VIEW v_project_kanban_stat AS
            SELECT
                p.name AS project_name,
                b.name AS board_name,
                SUM(CASE WHEN t.state = 'todo' THEN 1 ELSE 0 END) AS todo_count,
                SUM(CASE WHEN t.state = 'doing' THEN 1 ELSE 0 END) AS doing_count,
                SUM(CASE WHEN t.state = 'done' THEN 1 ELSE 0 END) AS done_count,
                SUM(CASE WHEN t.state = 'backlog' THEN 1 ELSE 0 END) AS backlog_count
            FROM project p
            JOIN board b ON b.project_id = p.id
            JOIN task t ON t.board_id = b.id 
            GROUP BY p.id, p.name, b.id, b.name
        """
        );

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
