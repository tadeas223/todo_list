using System.ComponentModel;
using Domain;
using Domain.Model;
using DI;

namespace Data;

public class CsvProjectDataImport : IDataImport
{
    private string path;
    public CsvProjectDataImport(string path)
    {
        this.path = path;
    }

    public void Import()
    {
        string[] lines = File.ReadAllLines(path);

        List<Project> projects = new();

        foreach (string line in lines)
        {
            string[] values = line.Split(';');

            Project project = new ProjectBuilder()
                .WithName(values[0])
                .WithLocked(values[1] == "true")
                .Build();

            projects.Add(project);
        }

        Provider.Instance.ProvideProjectRepository().InsertMany(projects);
    }
}