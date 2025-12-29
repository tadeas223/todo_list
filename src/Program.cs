using Data.Repository;
using DI;
using Domain;
using Domain.Model;

var con = Provider.Instance.ProvideDBConnection();

// for testing
//try
//{
//con.Delete("test", "password", "localhost:1521/XE", "database");
//} catch(Exception e)
//{
//    Console.WriteLine(e.Message);
//}
//con.Create("test", "password", "localhost:1521/XE", "database", "password");

con.Connect("database", "password", "localhost:1521/XE");

var projectRepo = Provider.Instance.ProvideProjectRepository();


//var project = new ProjectBuilder().WithName("proj").WithLocked(false).Build();
//projectRepo.Insert(ref project);

Console.WriteLine("Hello, World!");

var proj = projectRepo.SelectAll();
if(proj == null)
{
    Console.WriteLine("proj is null");
}

foreach(Project p in proj!)
{
    Console.WriteLine(p.Name);
    projectRepo.Delete(p);
}

Console.WriteLine("deleted");
proj = projectRepo.SelectAll();
if(proj == null)
{
    Console.WriteLine("proj is null");
}

foreach(Project p in proj!)
{
    Console.WriteLine(p.Name);
}

con.Disconnect();