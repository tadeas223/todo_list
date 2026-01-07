using System.ComponentModel;
using Domain;
using Domain.Model;
using DI;

namespace Data;

public class CsvTaskDataImport : IDataImport
{
    private string path;
    private Board board;
    public CsvTaskDataImport(Board board, string path)
    {
        this.path = path;
        this.board = board;
    }

    public void Import()
    {
        string[] lines = File.ReadAllLines(path);

        List<TodoTask> tasks = new();
        
        foreach (string line in lines)
        {
            string[] values = line.Split(';');

            TodoTask task = new TodoTaskBuilder()
                .WithBoard(board)
                .WithName(values[0])
                .WithDesc(values[1])
                .WithProgress((float)Convert.ToDouble(values[2]))
                .WithState((TaskState)Enum.Parse(typeof(TaskState), values[3].ToUpper()))
                .Build();

            tasks.Add(task);
        }

        Provider.Instance.ProvideTodoTaskRepository().InsertMany(tasks);

    }
}