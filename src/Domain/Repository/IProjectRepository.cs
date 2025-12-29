using Domain.Model;

namespace Domain.Repository;

public interface IProjectRepository
{
    public void Insert(Project project);
    public void Update(Project project);
    public void Delete(Project project);

    public HashSet<Project> SelectAll();
    public Project? SelectByName(string name);
    public Project? SelectById(int id);
}