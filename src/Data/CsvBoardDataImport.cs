using System.ComponentModel;
using Domain;
using Domain.Model;
using DI;

namespace Data;

public class CsvBoardDataImport : IDataImport
{
    private string path;
    private Project project;
    public CsvBoardDataImport(Project project, string path)
    {
        this.path = path;
        this.project = project;
    }

    public void Import()
    {
        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            string[] values = line.Split(';');

            Board board = new BoardBuilder()
                .WithProject(project)
                .WithName(values[0])
                .Build();

            Provider.Instance.ProvideBoardRepository().Insert(ref board);
        }

    }
}