using Data.Repository;
using Domain;

OracleDBConnection con = new();

// for testing
con.Connect("test", "password", "localhost:1521/XE");

Console.WriteLine("Hello, World!");

con.Disconnect();