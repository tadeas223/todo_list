namespace Domain.Model;

class Board
{
    private int id;
    private string name;

    public int Id => id;
    public string Name => name;

    public Board(BoardBuilder builder)
    {
        id = builder.Id;
        name = builder.Name;
    }

}