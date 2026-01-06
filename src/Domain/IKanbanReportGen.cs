namespace Domain;

using Domain.Model;

public interface IKanbanReportGen
{
    public List<KanbanReport> Generate();
}