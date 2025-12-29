namespace Domain.Model;

class BoardBuilder
{
    private int id;
    private string name;

    public int Id => id;

    public string Name
    {
        get { return name; }
        set {name = value; }
    } 

    public BoardBuilder(int id)
    {
        this.id = id;
        name = "";
    }

    public BoardBuilder(Board original)
    {
        id = original.Id;
        name = original.Name;
    }

    public BoardBuilder withName(string name)
    {
        Name = name;
        return this;
    }

    public Board build()
    {
        return new Board(this);
    }
}