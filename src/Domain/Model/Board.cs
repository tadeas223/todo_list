namespace Domain.Model;

public class Board
{
    private int id;
    private string name;
    private Project project;

    public int Id => id;
    public string Name => name;
    public Project Project => project;

    public Board(BoardBuilder builder)
    {
        id = builder.Id;
        name = builder.Name!;
        project = builder.Project!;
    }

}