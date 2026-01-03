using System.Dynamic;
using Domain.Model;

namespace Domain.Repository;

public interface IBoardRepository
{
    public void Insert(ref Board board);
    public void Update(Board board);
    public void Delete(Board board);
    
    public HashSet<Board> SelectAll();
    public Board? SelectById(int id);
    public HashSet<Board> SelectByProject(Project project);
}