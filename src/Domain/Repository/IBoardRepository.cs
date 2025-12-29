using System.Dynamic;
using Domain.Model;

namespace Domain.Repository;

public interface IBoardRepository
{
    public void Insert(Board board);
    public void Udate(Board board);
    public void Delete(Board board);
    
    public HashSet<Board> SelectAll();
    public Board SelectByName(Board board);
    public Board SelectById(Board board);
}